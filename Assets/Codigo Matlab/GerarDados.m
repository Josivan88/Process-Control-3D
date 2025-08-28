%------------------------------------------------------------------
% programa para geração de dados de predição em sistemas dinâmicos
%------------------------------------------------------------------

%Josivan 2019

function GerarDados

clc
clear

%geração do sinal
liminf=0.1;
limsup=140;
%Ksi = -1.5;
x=liminf:5:limsup;
y=func(x,liminf,limsup); %definida no fim do script
%aplitude do ruído
amp=0.02*rand(1).*range(y);
disp('Resultados');
disp(' ');
fprintf('Amplitude do ruido: %5.5f',amp);
disp(' ');
ruido=amp*(rand(1,length(x))-1/2);
%combinação
yr=y+ruido;

%Gráfico
  plot(x,y,x,yr);
  grid on;
  legend('sinal limpo','sinal com ruido');
  %saveas(gcf,'Grafico.png')

%gerando dados de entrada e saída da rede
%entradas
NumEnt=7; %Número de pontos para treinamento;
disp(' ');
%fprintf('Número de casos %5.0f',length(yr)-NumEnt+1);%mostra quantos casos de treinamento foram gerados
disp(' ');
Input=zeros(NumEnt,length(yr)-NumEnt-1);
Output=zeros(1,length(yr)-NumEnt-1);
for i=1:length(yr)-NumEnt-1
    for j=1:NumEnt
       Input(j,i)=yr(i+j);
    end
    Output(1,i)=y(i+j+1);
    %Output(2,i)=Ksi;
end

fid = fopen('Data.csv','wt');
fprintf(fid,'Nome\n');
fprintf(fid,'NC:%3.0f',length(yr)-NumEnt-1); %Número de casos
fprintf(fid,' AR: %5.4f',amp); %Amplitude do ruido
fprintf(fid,'\nEntradas\n');
fclose(fid);
dlmwrite('Data.csv',Input,'precision','%.6f','-append','newline','pc');
fid = fopen('Data.csv','a+');
fprintf(fid,'\nSaidas\n');
fclose(fid);
dlmwrite('Data.csv',Output,'precision','%.6f','-append','roffset',1,'newline','pc');
type('Data.csv');


function u0 = func(x,liminf,limsup)
u0=zeros(1,length(x));
for i=1:length(x)
if (x(i)>=liminf) && (x(i)<=(liminf+limsup)/2)
    %u0(i) = 0.0001*exp(0.1.*x(i)).*sin(-0.2.*x(i))+0.2;   %MODIFICAR
    u0(i) = 0;
end
if (x(i)>(liminf+limsup)/2) && (x(i)<=limsup)
    %u0(i) = 0.0001*exp(0.1.*x(i)).*sin(-0.2.*x(i))+0.2;   %MODIFICAR
    u0(i) = 1-exp(-0.1.*(x(i)-45));
end
end




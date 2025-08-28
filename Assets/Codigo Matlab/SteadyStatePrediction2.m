clc
clear

%utilizar double
%poucos pontos (4-12) e curvatura considerável melhoram o resultado
%reamostrar todo o intervalo para poucos pontos (4-12), assim ter uma curvatura maior
%utilizar uma interpolação (cubica? linear?)


ExactA=50;
ExactB=-60;
ExactK=0.1; %K grande ainda é um problema
Noise=5;
t=1:2:20;     
t2=1:1:42;
y=ExactB+ExactA*exp(-ExactK.*t)+Noise*(rand(1,length(t))-0.5);

Cicles=15;

LogTerm=zeros(1,length(y)-2);
MatrizK=zeros(1,length(t)-2);
EstK=0.05;
MeanK=0.01;
meanIndex = round(length(y)/2);

for j=1:Cicles

    for i=1:length(t)-2
        LogTerm(i) = ((y(i+1)-y(i))*(t(i+2)-t(i+1)))/((y(i+2)-y(i+1))*(t(i+1)-t(i)));
        if (LogTerm(i)>0)
            MatrizK(i)=abs((2/(t(i+2)-t(i)))*log(LogTerm(i))+EstK)/2;
            MatrizK(i)=((2/(t(i+2)-t(i)))*log(LogTerm(i))+3*MatrizK(i))/4;
        end
        if (MatrizK(i)==0)
            MatrizK(i)=mean(MatrizK);
        end
    end

    EstK=abs(2*EstK+mean(MatrizK))/3;
    if EstK==0 %Mal condicionamento no ajuste linear
        EstK=MeanK;
    else
        MeanK=abs(MeanK+EstK)/2;
    end

end

EstK

%Estimativa inicial para A e B
A=(y(length(y))-y(1))/(exp(-EstK*t(length(y)))-exp(-EstK*t(1)));
B=y(1)-(A*exp(-EstK.*t(1)));
Aaux=A;
Baux=y(length(y))-(A*exp(-EstK.*t(length(y))));

for j=1:Cicles
    for i=1:length(t)-1
       Aaux=(y(i+1)-y(1))/(exp(-EstK*t(i+1))-exp(-EstK*t(1)));
       A=(A+Aaux)/2;
       Baux=y(i)-(A*exp(-EstK.*t(i)));
       B=(B+Baux)/2;
       if((y(i)-B)/A)>0
            EstKAux=-(1/t(i))*log(abs((y(i)-B)/A));
            if EstKAux>0
                EstK=(EstK+EstKAux)/2;
            end
       end
    end
end

EstK
A
B

yc=B+A*exp(-EstK.*t);
r2= corrcoef(y,yc);
ErroMedio=sqrt(mean(sum((y-yc).^2)))/4;
ErroPercentual=(ErroMedio*100)/(max(y)-min(y));
r2(1,2)                                %precisa ser maior que 0,99 para ser considerado bom
TempoEst=(4/EstK)-(t(length(y))-t(1))  %Tempo que falta para atingir 98% do valor estacionário, 
%retirar média com valores anteriores, 
%não funciona bem para sistemas lentos

plot(t,y,t2,B+A*exp(-EstK.*t2));

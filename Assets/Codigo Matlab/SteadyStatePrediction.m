clc
%clear
ExactA=-7;
ExactB=37;
ExactK=0.02;
Noise=0.1;
t=1:1:11;
t2=1:1:20;
y=ExactB+ExactA*exp(-ExactK.*t)+Noise*(rand(1,length(t))-0.5);
ys = zeros(1,length(t));

LogTerm=zeros(1,length(y)-2);

% A=0;
% B=0;
% MatrizK=zeros(1,length(t)-2);
% EstK=0;
% MeanK=0.0001;

for j=1:2
for i=1:length(y)
if(i>1) && (i<length(y)-1)
    ys(i)=0.2*y(i-1)+0.6*y(i)+0.2*y(i+1);
end
if(i<2)
    ys(i)=(y(i)+y(i+1))/2;
end
if(i>length(y)-2)
    ys(i)=(y(i)+y(i-1))/2;
end
end
end

r2= corrcoef(y,ys);
r2(1,2)
EstNoise=sqrt(mean(sum((y-ys).^2)))/10;

for i=1:length(t)-2
    %LogTerm(i) = ((y(i+2)+y(i+1))*(y(i+1)-y(i))*(t(i+2)-t(i+1)))/((y(i+2)^2-y(i+1)^2)*(t(i+1)-t(i)));
    LogTerm(i) = ((y(i+1)^2-y(i)^2)*(t(i+2)-t(i+1)))/((y(i+1)+y(i))*(y(i+2)-y(i+1))*(t(i+1)-t(i)));
end
% for j=1:1
% for i=1:length(t)-3
%         LogTerm(i) = (LogTerm(i)+LogTerm(i+1))/2;
% end
% end
for i=1:length(t)-2
    if (LogTerm(i)>0)
        MatrizK(i)=((2/(t(i+2)-t(i)))*log(LogTerm(i))+EstK)/2;
    end
end
for i=1:length(t)-3
    if (MatrizK(i)==0)
        MatrizK(i)=mean(MatrizK);
    end
end

disp('K')
EstK=abs(2*EstK+mean(MatrizK))/3
if EstK==0 %Mal condicionamento no ajuste linear
    EstK=MeanK;
else
    MeanK=abs(MeanK+EstK)/2;
end
rcut=1/(60*EstK)
%rcut=0.993*sqrt(sqrt(sqrt(EstK)))
1/(50*EstK)

% if r2(1,2)>rcut || r2(1,2)>0.993
if r2(1,2)>0.993 || r2(1,2)>1/(60*EstK)
    disp('EstK')
    P = polyfit(exp(-EstK.*t),y,1);
    %yfit = P(1)*t+P(2);
    %mean(LogTerm)
    A=(P(1)+A)/2;
    B=(P(2)+B)/2;
    if A>0
    if P(2)<y(length(y))
            B=(P(2)+B)/2;
    end
    end
    if A<0
        if P(2)>y(length(y))
            B=(P(2)+B)/2;
        end
    end
    factor=y(1)/(B+A*exp(-EstK.*t(1)));

    A=factor*A
    B=factor*B
end


plot(t,y,t2,B+A*exp(-EstK.*t2));

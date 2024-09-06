docker build -f services/TechHoy.Logger.Service/Dockerfile -t th-logger .
docker run -p 10900:10900 -v "C:/01_TMP:/mnt/db"  --rm -it th-logger

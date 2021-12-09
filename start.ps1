#evt create password for user string and pass it to user secrest?
#is that possible


#start the database:
docker run -d -e POSTGRES_USER=dev -e POSTGRES_PASSWORD=password123 --name Yoghurtbase -p 5432:5432 --restart=always postgres

#build the omage from dockerfile:
Docker build -t test -f DockerFile .


set -e

aws ecr get-login-password --region us-east-2 --profile todo-list-admin | docker login --username AWS --password-stdin 964894216790.dkr.ecr.us-east-2.amazonaws.com/todo-list-api:latest
docker build -f ./Dockerfile -t todo-list-api:latest .
docker tag todo-list-api:latest 964894216790.dkr.ecr.us-east-2.amazonaws.com/todo-list-api:latest
docker push 964894216790.dkr.ecr.us-east-2.amazonaws.com/todo-list-api:latest

Write-Host "AWS Ecr repo updated"
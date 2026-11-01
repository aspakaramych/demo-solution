# DemoSolution

Для запуска нужен .Net 10 версии и из корня проекта
```bash
dotnet run
```

Можно спуллить Docker образ 

```bash
docker pull aspakaramych/demo_solution_opp:latest
```
и запустить 

```bash
docker run -d --name demo_solution_opp -p 8080:8080 aspakaramych/demo_solution_opp:latest
```

После перейти на ручку /scalar где есть все ендпоинты приложения

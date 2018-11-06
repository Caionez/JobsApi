# JobsApi

.NET Core Web API

*Requires .NET Core 2.1*

- To **run the api**, execute: `dotnet run`
- To **run the Unit Tests** execute: `dotnet test JobsApi.Tests/JobsApi.Tests.csproj`

## Example calls:

### Jobs

**GET ALL JOBS**

`GET {{HOST}}/api/jobs`

Sorted by weight:
`GET {{HOST}}/api/jobs?sortByWeight=true`

**POST**

`POST {{HOST}}/api/jobs`

Body:
```json
{
  "id": 1,
  "name": "Job 1",
  "active": true,
  "tasks": [
    {
      "id": 1,
      "name": "Task 1",
      "weight": 5,
      "completed": true,
      "createdAt": "2008-05-22"
    },
    {
      "id": 2,
      "name": "Task 2",
      "weight": 3,
      "completed": true,
      "createdAt": "2013-12-23"
    }
  ]
}
```

**GET BY ID**

`GET {{HOST}}/api/jobs/1`

**PUT**

`PUT {{HOST}}/api/jobs/1`

Body:
```json
{
  "id": 1,
  "name": "Edited Job 1",
  "active": true,
  "tasks": [
    {
      "id": 1,
      "name": "Edited Task 1",
      "weight": 5,
      "completed": false,
      "createdAt": "2018-05-22"
    },
    {
      "id": 2,
      "name": "Edited Task 2",
      "weight": 5,
      "completed": false,
      "createdAt": "2018-12-23"
    }
  ]
}
```

**DELETE**

`DELETE {{HOST}}/api/jobs/1`

### Tasks

**GET ALL TASKS**

`GET {{HOST}}/api/tasks`

Created at date:
`GET {{HOST}}/api/tasks?createdAt=2018-01-01`

**POST**

`POST {{HOST}}/api/tasks`

Body:
```json
{
  "id": 1,
  "name": "Task 1",
  "weight": 5,
  "completed": true,
  "createdAt": "2008-05-22"
}
```

**GET BY ID**

`GET {{HOST}}/api/tasks/1`

**PUT**

`PUT {{HOST}}/api/tasks/1`

Body:
```json
{
  "id": 1,
  "name": "Edited Task 1",
  "weight": 5,
  "completed": false,
  "createdAt": "2018-05-22"
}
```

**DELETE**

`DELETE {{HOST}}/api/tasks/1`

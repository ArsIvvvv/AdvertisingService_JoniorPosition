 Веб-сервис для управления рекламными площадками

## Описание

REST API сервис для хранения и поиска рекламных площадок по географическим локациям.


## Установка

```bash
git clone https://github.com/ArsIvvvv/AdvertisingService_JoniorPosition.git
cd ваш-репозиторий
dotnet run --urls "https://localhost:7239"
и перейдите "https://localhost:7239"/swagger
```

Создайте json-файл
Пример json-файла

```json
{
  "platforms": [
    {
      "name": "Яндекс.Директ",
      "locations": [ "/ru" ]
    },
    {
      "name": "Ревдинский рабочий",
      "locations": [ "/ru/svrd/revda", "/ru/svrd/pervik" ]
    },
    {
      "name": "Газета уральских москвичей",
      "locations": [ "/ru/msk", "/ru/permobl", "/ru/chelobl" ]
    },
    {
      "name": "Крутая реклама",
      "locations": [ "/ru/svrd" ]
    }
  ]
}
```
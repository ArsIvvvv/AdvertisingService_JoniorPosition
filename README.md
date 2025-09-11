 ���-������ ��� ���������� ���������� ����������

## ��������

REST API ������ ��� �������� � ������ ��������� �������� �� �������������� ��������.


## ���������

```bash
git clone https://github.com/ArsIvvvv/AdvertisingService_JoniorPosition.git
cd ���-�����������
dotnet run --urls "https://localhost:7239"
� ��������� "https://localhost:7239"/swagger
```

�������� json-����
������ json-�����

```json
{
  "platforms": [
    {
      "name": "������.������",
      "locations": [ "/ru" ]
    },
    {
      "name": "���������� �������",
      "locations": [ "/ru/svrd/revda", "/ru/svrd/pervik" ]
    },
    {
      "name": "������ ��������� ���������",
      "locations": [ "/ru/msk", "/ru/permobl", "/ru/chelobl" ]
    },
    {
      "name": "������ �������",
      "locations": [ "/ru/svrd" ]
    }
  ]
}
```
«API Рабочего Места Библиотекаря».

<a target="_blank" href="http://46.180.95.110:1325/">DEMO</a>

1. Microsoft Entity Framework Core (9.0.3).
2. PostgreSQL 17).
3. VUE 3.
4. BACK-END – WebAPI (.NET 9).

1.	API реализует следующие методы:
1.1.	Работа с книгами.
- добавить новую книгу
- изменить данные о книге
- удалить данные о книге

- получить данные о книге по ID
- получить список выданных книг
- получить список доступных для выдачи книг
- поиск книг(и) по наименованию (части наименования)

1.2.	Работа с читателями.
- добавить нового читателя
- изменить данные о читателе
- удалить данные о читателе

- выдать книгу читателю
- сдать книгу в библиотеку

- получить данные о читателе по ID (по возможности со списком выданных книг)
- поиск читателя(-ей) по ФИО (части ФИО) (по возможности со списком выданных книг)

2.	Объект «Книга» обладает следующими атрибутами: 
«Наименование»
«Автор» (для хранения использовать одно поле строкового типа)
«Артикул» 
«Год издания» 
«Количество экземпляров» 

3.	Объект «Читатель» обладает атрибутами: 
«ФИО»
«Дата рождения»

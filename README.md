1) ConnectionString поправить в

\UI\ISERV.UI.API\appsettings.json
\Persistence\ISERV.Persistence.EFMigrations\appsettings.json
\Loader\ISERV.Loader.Console\appsettings.json

3) Создать БД :
   запустить приложение "ISERV.Persistence.EFMigration"
   
4) Запусить загрузку с сайта
   в файле "\Loader\ISERV.Loader.Console\appsettings.json" изменить количесво потоков
   запустить приложение "ISERV.Loader.Console"

5) API для поиска по Country , Name
   Запустить "ISERV.UI.API" (на SwaggerUI)

6) Скрипт БД : "schema_bd.sql"

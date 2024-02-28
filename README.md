1) ConnectionString поправить в

   \UI\ISERV.UI.API\appsettings.json<br />
   \Persistence\ISERV.Persistence.EFMigrations\appsettings.json<br />
   \Loader\ISERV.Loader.Console\appsettings.json<br />


3) Создать БД :<br />

   запустить приложение "ISERV.Persistence.EFMigration"
   
4) Запусить загрузку с сайта<br />

   в файле "\Loader\ISERV.Loader.Console\appsettings.json" изменить количесво потоков<br />

   запустить приложение "ISERV.Loader.Console"

5) API для поиска по Country , Name<br />

   Запустить "ISERV.UI.API" (на SwaggerUI)<br />


6) Скрипт БД : "schema_bd.sql"<br />


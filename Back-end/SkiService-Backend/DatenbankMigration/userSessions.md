# Datenmigrationimport-Anleitung für die Kollektion "registrations" in MongoDB
Bevor du dieses Projekt ausführen möchtest, musst du erst alle Schritte im Ordner "DatenbankMigration" durchführen.

Diese Anleitung führt dich durch die Schritte, die erforderlich sind, um "userInfos" Kollektion in die MongoDB-Datenbank zu importieren. Bitte folge den Anweisungen sorgfältig.

## Vorbereitung

Bevor du den Importbefehl ausführst, stelle sicher, dass MongoDB auf deinem System installiert ist und ordnungsgemäss funktioniert. 


## Schritte zum Datenimport

### Schritt 1: Öffne die Eingabeaufforderung (CMD)

Öffne die Eingabeaufforderung auf deinem Computer. Dies kannst du tun, indem du `cmd` in das Suchfeld deines Startmenüs eingibst und `Enter` drückst.

### Schritt 2: Wechsle in das richtige Verzeichnis

Zuerst Navigieren wir mit `cd ..` ausserhalb dem User indem wir gerade stehen.

Danach Navigiere in der Eingabeaufforderung in das Verzeichnis, das die zu importierende Datei enthält. Verwende dazu den `cd` Befehl.

```bash
cd <Pfad-zum-Verzeichnis>
 
Beispiel bei mir: cd C:\Github\M165Projekt\M165Azin\Back-end\SkiService-Backend
```

### Schritt 3: Den Importbefehl ausführen
Im CMD gibst du dann diesen MongoImport-Befehl ein: mongoimport --uri mongodb://<DeinServer>:<Port>/<DeineDatenbank> --collection userSessions --type json --file <PfadZurImportdatei> --jsonArray. Dieser muss dann korrekt angepasst werden.

Mein Beispiel: ´mongoimport --uri mongodb://localhost:27017/M165Azin --collection userSessions --type json --file C:\Github\M165Projekt\M165Azin\Back-end\SkiService-Backend\DatenbankImports\DatenbankImport3.txt --jsonArray´


### Schritt 4: In der MongoDb die Kollektion abrufen.
Mit ´db.userSessions.find()´ siehst du all den Inhalt in der userSessions Kollektion.
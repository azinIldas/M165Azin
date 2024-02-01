db.runCommand({
    collMod: "registrations",
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["name", "email", "tel", "priority", "service", "startDate", "finishDate", "status", "note"],
            properties: {
                _id: {
                    bsonType: "objectId",
                    description: "muss eine gültige ObjectId sein"
                },
                ID: {
                    bsonType: "int",
                    minimum: 0,
                    description: "muss eine nicht negative ganze Zahl sein"
                },
                name: {
                    bsonType: "string",
                    minLength: 5,
                    maxLength: 100,
                    description: "muss ein String sein und ist erforderlich"
                },
                email: {
                    bsonType: "string",
                    pattern: "^.+@.+\..+$",
                    description: "muss ein gültiges E-Mail-Format haben und ist erforderlich"
                },
                tel: {
                    bsonType: "string",
                    minLength: 8,
                    maxLength: 20,
                    description: "muss ein String sein und ist erforderlich"
                },
                priority: {
                    bsonType: "string",
                    enum: ["Niedrig", "Mittel", "Hoch"],
                    description: "kann nur einer der drei angegebenen Werte sein"
                },
                service: {
                    bsonType: "string",
                    description: "muss ein String sein und ist erforderlich"
                },
                startDate: {
                    bsonType: "date",
                    description: "muss ein Datum sein und ist erforderlich"
                },
                finishDate: {
                    bsonType: "date",
                    description: "muss ein Datum sein und ist erforderlich"
                },
                status: {
                    bsonType: "string",
                    enum: ["Offen", "In Bearbeitung", "Erledigt", "Storniert"],
                    description: "muss ein gültiger Status sein"
                },
                note: {
                    bsonType: "string",
                    minLength: 0,
                    maxLength: 500,
                    description: "muss ein String sein und darf maximal 500 Zeichen haben"
                }
            }
        }
    },
    validationAction: "warn" 
});

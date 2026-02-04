# AgentBuildTest - ToDo-Liste Windows Anwendung

Eine einfache ToDo-Listen-Anwendung für Windows, entwickelt mit C# und Windows Forms.

## Features

- ✅ Aufgaben hinzufügen
- ✅ Aufgaben als erledigt markieren
- ✅ Aufgaben löschen
- ✅ Moderne Benutzeroberfläche
- ✅ Tastaturunterstützung (Enter zum Hinzufügen)
- ✅ **Integriertes Tracing und Logging**

## Voraussetzungen

- .NET 8.0 SDK oder höher
- Windows Betriebssystem

## Installation & Start

1. Projekt kompilieren:
```bash
dotnet build
```

2. Anwendung starten:
```bash
dotnet run
```

## Verwendung

1. Geben Sie eine neue Aufgabe in das Textfeld ein
2. Klicken Sie auf "Hinzufügen" oder drücken Sie Enter
3. Wählen Sie eine Aufgabe aus der Liste aus
4. Klicken Sie auf "Als erledigt markieren" um die Aufgabe abzuhaken
5. Klicken Sie auf "Löschen" um eine Aufgabe zu entfernen

## Tracing und Logging

Die Anwendung verfügt über ein integriertes Tracing-System, das alle wichtigen Ereignisse protokolliert:

### Trace-Ausgabe
- **Konsole**: Trace-Ereignisse werden in der Konsole ausgegeben (wenn von der Konsole gestartet)
- **Log-Datei**: `%APPDATA%\ToDoList\trace.log`

### Protokollierte Ereignisse
- Anwendungsstart und -beendigung
- Formular-Initialisierung
- Benutzerinteraktionen:
  - Hinzufügen von Aufgaben (mit Validierung)
  - Markieren von Aufgaben als erledigt/unerledigt
  - Löschen von Aufgaben (mit Bestätigung)
- Fehler und Warnungen

### Trace-Level
Die Anwendung verwendet verschiedene Trace-Level:
- **Information**: Normale Operationen (Start, Stop, erfolgreiche Aktionen)
- **Verbose**: Detaillierte Informationen über Benutzeraktionen
- **Warning**: Validierungsfehler und Benutzerhinweise
- **Error**: Anwendungsfehler und Ausnahmen

### Log-Datei finden
Die Log-Datei befindet sich unter:
```
Windows: C:\Users\<Username>\AppData\Roaming\ToDoList\trace.log
```

## Technologie

- C# 12
- .NET 8.0
- Windows Forms
- System.Diagnostics (Tracing)

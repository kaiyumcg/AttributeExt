# AttributeExt
A collection of useful attributes to decorate your classes, methods, fields.

####Current features
* readonly
* toggle
* method button
* reference(interface/abstract method etc) serialization
* tag selection

#### Installation:
Add an entry in your manifest.json as follows:
```C#
"com.kaiyum.attributeext": "https://github.com/kaiyumcg/AttributeExt.git"
```

Since unity does not support git dependencies, you need the following entry as well:
```C#
"com.kaiyum.editorutil": "https://github.com/kaiyumcg/EditorUtil.git"
```
Add it into your manifest.json file in "Packages\" directory of your unity project, if it is already not in the manifest.json file.
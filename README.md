RRON: Rhythm represented object notation
A custom serialiser/deserializer for representing Rhythm game data.
it is meant to get rid of the overhead that json provides, but isnt meant to be a catch all, like json (so it doesnt work for every object)
it is meant for things like settings and beatmaps

Simple:
```
Name: value
```
Collections: 
```
[Name]
value1, value2, value3, ...
```
Complex:
```
[ClassName: PropName1, PropName2, PropName3, ...]
valueOfProp1, valueOfProp2, valueOfProp3, ...
```
ComplexCollection:
```
[[ClassName: PropName1, PropName2, PropName3, ...]
value1ofProp1, value1ofProp2, value1ofProp3, ...
value2ofProp1, value2ofProp2, value2ofProp3, ...
value3ofProp1, value3ofProp2, value3ofProp3, ...
...
]
```
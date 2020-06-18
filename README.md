# Turtle in a minefield

The minefield can contain the following type of tiles:
- clear tiles
- mines
- exit tile

The 'turtle' can turn in 90 degrees clockwise or step forward. 

The goals:
- process the input file with the field setup
- process the input file with moves
- calculate the end result

The end result:
- turtle stays in danger - no moves, clear tile
- turtle exits the field by reaching the exit tile
- turtle blows up by stepping on a mine tile

Example field setup JSON input:
```json
{
  "boardSize": {
    "x": 5,
    "y": 4
	},
  "startingPoint": {
    "x": 0,
    "y": 1,
    "d": "North"
  },
  "exitPoint": {
    "x": 4,
    "y": 2
  },
  "mines": [ {
    "x": 1,
    "y": 1
  },{
    "x": 3,
    "y": 1
  },{
    "x": 3,
    "y": 3
  } 
  ]
}
```

Example moves JSON input:
```json
[	
  [ "Rotate", "Move" ],
  [ "Move", "Move", "Move"],
  [ "Move", "Rotate", "Move", "Move", "Move", "Move", "Rotate", "Move", "Move" ]
]
```

Based on the above the setup and moves list, the result is as follows:
- turtle turns and steps on a mine
- turtle moves towards the edge, skips turns when attempting to move forward over the edge
- exits the field successfully

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<GameObject[,]> prevGrids = new List<GameObject[,]>();
    public GameObject[,] grid = new GameObject[5, 5];
    public float cellSize;
    public Vector2 offset;
    public List<GameObject> allTiles = new List<GameObject>();
    public GameObject inGamePlayer;
    public UIManager ui;
    List<GridObject.BlockType> variables = new List<GridObject.BlockType>
    {
        GridObject.BlockType.speed,
        GridObject.BlockType.jump,
        GridObject.BlockType.start, 
        GridObject.BlockType.setting,
        GridObject.BlockType.exit,
    };
    
    List<GridObject.BlockType> acceptsNumber = new List<GridObject.BlockType>()
    {
        GridObject.BlockType.speed,
        GridObject.BlockType.jump
    };

    List<GridObject> activeVariables = new List<GridObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < grid.GetLength(0); x++){
            for(int y = 0; y < grid.GetLength(1); y++){
                //debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, getWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 5, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x, y+1), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x + 1, y),  Color.white, 100f);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideObject(Vector2Int coords , Vector2Int dir)
    {            
        if(grid[coords.x, coords.y].GetComponent<GridObject>().movable == false){
            return;
        }

        GameObject block = grid[coords.x, coords.y];

        Vector2Int checkCoord = coords;
        while(true){
            checkCoord += dir;

            if(checkCoord.x > 4 || checkCoord.x < 0 || checkCoord.y > 4 || checkCoord.y < 0){
                if(coords != checkCoord - dir){
                    grid[(checkCoord - dir).x, (checkCoord - dir).y] = block;
                    block.GetComponent<GridObject>().slideToPos((checkCoord - dir));
                    grid[coords.x, coords.y] = null;
                }
                break;
                
                
            }
            
            if(grid[checkCoord.x, checkCoord.y] != null)
            {
                if(coords != checkCoord - dir){
                    grid[(checkCoord - dir).x, (checkCoord - dir).y] = block;
                    block.GetComponent<GridObject>().slideToPos((checkCoord - dir));
                    grid[coords.x, coords.y] = null;
                    grid[checkCoord.x, checkCoord.y].GetComponent<GridObject>().OnCollide();
                }
                break;
            }
            
        }
    }

    public bool checkMovable(Vector2Int coord){
        if(coord.x > 5 || coord.x < -1 || coord.y < -1 || coord.y > 5){
            return false;
        }
        else if(coord.x > 4 || coord.x < 0 || coord.y < 0 || coord.y > 4){
            return true;
        }
        else if(grid[coord.x, coord.y] == null){
            return true;
        }
        else{
            return false;
        }
    }

    public void UpdateGrid(){
        grid = new GameObject[5, 5];
        foreach(GameObject tile in allTiles){
            grid[tile.GetComponent<GridObject>().x , tile.GetComponent<GridObject>().y] = tile;
        }
        CheckMatches();
    }

    public GameObject checkObject(Vector2Int coord)
    {
        if(coord.x > 4 || coord.x < 0 || coord.y < 0 || coord.y > 4){
            return null;
        }

        return grid[coord.x, coord.y];
    }

    public Vector2 getWorldPosition(int x, int y){
        return new Vector2(x, y) * cellSize + new Vector2(0.5f, 0.5f) * cellSize + offset;
    }

    public Vector2Int getXY(Vector2 pos){
        pos = pos - offset;
        return new Vector2Int((int)Mathf.Round((pos.x-cellSize/2)/cellSize), (int)Mathf.Round((pos.y-cellSize/2)/cellSize));
    }


    public void SetupPosition()
    {
        for(int x = 0; x < grid.GetLength(0); x++){
            for(int y = 0; y < grid.GetLength(1); y++){
                if(grid[x, y] != null){
                    if(grid[x, y].GetComponent<GridObject>().gridManager == null){
                        grid[x, y].GetComponent<GridObject>().gridManager = this;
                    }
                    grid[x, y].GetComponent<GridObject>().x = x;
                    grid[x, y].GetComponent<GridObject>().y = y;
                    grid[x, y].GetComponent<GridObject>().snapToPos();
                }
            }
        }
    }

    public void SaveTilePos(){
        foreach(GameObject tile in allTiles){

            tile.GetComponent<GridObject>().savePos();
        }
    }

    public void RewindTilePos(){
        foreach(GameObject tile in allTiles){
            tile.GetComponent<GridObject>().rewind();
            UpdateGrid();
        }
    }

    public void Reset(){
         foreach(GameObject tile in allTiles){
            tile.GetComponent<GridObject>().ResetToDefault();
        }
    }

    
    
    public void PutAway(){
        foreach(GameObject tile in allTiles){
            tile.GetComponent<GridObject>().prevPos = new List<Vector2Int>();
        }
    }

    public void CheckMatches(){
        activeVariables.Clear();
        ui.quitAction = UIManager.menuOptions.none;
        inGamePlayer.GetComponent<PlayerMovemetnPhysics>().speed = 0;
        inGamePlayer.GetComponent<PlayerMovemetnPhysics>().jumpForce = 0;
        ui.playAction = UIManager.menuOptions.none;
        ui.settingsAction = UIManager.menuOptions.none;

        foreach(GameObject tile in allTiles){
            int x = tile.GetComponent<GridObject>().x;
            int y = tile.GetComponent<GridObject>().y;

            if(variables.Contains(tile.GetComponent<GridObject>().type)){
                if(x+2 < 5){
                    if(grid[x+1, y] != null && grid[x+2, y] != null){
                        if(grid[x+1, y].GetComponent<GridObject>().type == GridObject.BlockType.arrow){
                            if(acceptsNumber.Contains(tile.GetComponent<GridObject>().type)){
                                if(grid[x+2, y].GetComponent<GridObject>().type == GridObject.BlockType.num){
                                    if(tile.GetComponent<GridObject>().type == GridObject.BlockType.speed){
                                        inGamePlayer.GetComponent<PlayerMovemetnPhysics>().speed = grid[x+2, y].GetComponent<GridObject>().num * 2;
                                        activeVariables.Add(tile.GetComponent<GridObject>());
                                    }
                                    else{
                                        inGamePlayer.GetComponent<PlayerMovemetnPhysics>().jumpForce = grid[x+2, y].GetComponent<GridObject>().num * 10;
                                        if(x + 3 < 5){
                                            if(grid[x+3, y] != null){
                                                if(grid[x+3, y].GetComponent<GridObject>().type == GridObject.BlockType.num){
                                                    inGamePlayer.GetComponent<PlayerMovemetnPhysics>().jumpForce = 55 * 10;
                                                }
                                            }
                                        }
                                        activeVariables.Add(tile.GetComponent<GridObject>());
                                    }
                                }
                            }
                            else{
                                UIManager.menuOptions action = UIManager.menuOptions.none;
                                if(grid[x+2, y].GetComponent<GridObject>().type == GridObject.BlockType.start){
                                    action = UIManager.menuOptions.play;
                                }
                                if(grid[x+2, y].GetComponent<GridObject>().type == GridObject.BlockType.setting){
                                    action = UIManager.menuOptions.settings;
                                }
                                if(grid[x+2, y].GetComponent<GridObject>().type == GridObject.BlockType.exit){
                                    action = UIManager.menuOptions.quit;
                                }

                                if(tile.GetComponent<GridObject>().type == GridObject.BlockType.start){
                                    ui.playAction = action;
                                    activeVariables.Add(tile.GetComponent<GridObject>());
                                }
                                if(tile.GetComponent<GridObject>().type == GridObject.BlockType.setting){
                                    ui.settingsAction = action;
                                    activeVariables.Add(tile.GetComponent<GridObject>());
                                }
                                if (tile.GetComponent<GridObject>().type == GridObject.BlockType.exit){
                                    ui.quitAction = action;
                                    activeVariables.Add(tile.GetComponent<GridObject>());
                                }
                            }
                        }
                    }
                }
            }
        }

        UpdateBlinking();
    }

    void UpdateBlinking(){        
        List<GameObject> triggeredList =new List<GameObject>();
        foreach(GridObject block in activeVariables){
            block.activated = true;
            grid[block.x + 1, block.y].GetComponent<GridObject>().activated = true;
            grid[block.x + 2, block.y].GetComponent<GridObject>().activated = true;
            triggeredList.Add(block.gameObject);
            triggeredList.Add(grid[block.x + 1, block.y]);
            triggeredList.Add(grid[block.x + 2, block.y]);
        }

        foreach(GameObject tile in allTiles){
            if(!triggeredList.Contains(tile)){
                tile.GetComponent<GridObject>().activated = false;
            }
        }


    }
}

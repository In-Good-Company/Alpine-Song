using UnityEngine;
using UnityEngine.UI;
//Create Items through scriptable objects that the player can add to their inventory

    //Allows creating 'items' in the inspector where we can define the properties of the object than put it in the scene
[CreateAssetMenu(fileName = "New Item", menuName ="Invetory/Item")]
public class Item : ScriptableObject
{

    


    new public string name = "New Item";
    public Sprite icon = null;

    

}

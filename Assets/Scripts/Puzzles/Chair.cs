using UnityEngine;

public class Chair : DraggableItem
{
    private Info info;



    protected override void Start()
    {
        base.Start();
        info = GameManager.Instance.PuzzleController.ChairInfo;
        if (info.isReachedRight)
        {
            transform.position = new Vector2(rightEnd.position.x, transform.position.y);
        }
    }



    protected override void ReachedLeftEnd()
    {
        base.ReachedLeftEnd();
        info.isReachedRight = false;
    }



    protected override void ReachedRightEnd() 
    {
        base.ReachedRightEnd();
        info.isReachedRight = true;
    }



    public class Info
    {
        public bool isReachedRight = false;
    }
}

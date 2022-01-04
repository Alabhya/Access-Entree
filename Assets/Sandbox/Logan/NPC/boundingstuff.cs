using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundingstuff
{
    public float front;
    public float back;
    public float side;
    private Transform t;
    private Vector3 np;

    public boundingstuff(float front, float back, float side, Transform t, Vector3 np)
    {
        this.front = front;
        this.back = back;
        this.side = side;
        this.t = t;
        this.np = np;
    }

    public void Update(Transform t, Vector3 np)
    {
        this.t = t;
        this.np = np;
    }

    public List<Vector3> Steparound(Vector3 point)
    {
        List<Vector3> result = new List<Vector3>();
        Vector3 connect = point - np;
        float forwardp = Vector3sum(Vector3.Project(connect, t.forward)) / Vector3sum(t.forward);
        float sidep = Vector3sum(Vector3.Project(connect, t.right)) / Vector3sum(t.right);
        if (forwardp > 0)
        {
            if (sidep > 0)
            {
                result.Add(t.position + side * t.right);
                result.Add(t.position + front * t.forward);
            }
            else if (sidep < 0)
            {
                result.Add(t.position + front * t.forward);
                result.Add(t.position - side * t.right);
            }
            else
            {
                result.Add(t.position + front * t.forward);
            }
        }
        else if (forwardp < 0)
        {
            if (sidep > 0)
            {
                result.Add(t.position - back * t.forward);
                result.Add(t.position + side * t.right);
            }
            else if (sidep < 0)
            {
                result.Add(t.position - side * t.right);
                result.Add(t.position - back * t.forward);
            }
            else
            {
                result.Add(t.position - back * t.forward);
            }
        }
        else
        {
            if (sidep > 0)
            {
                result.Add(t.position + side * t.right);
            }
            else if (sidep < 0)
            {
                result.Add(t.position - side * t.right);
            }
        }
        return result;
    }

    public Vector3 CD(Vector3 point, Vector3 cp)
    {
        Vector3 connect = point - np;
        Vector3 pplink = cp - np;
        if (connect.y > 5)
            return Vector3.zero;
        float forwardp = Vector3sum(Vector3.Project(connect, t.forward)) / Vector3sum(t.forward);
        float forwardpp = Vector3sum(Vector3.Project(pplink, t.forward)) / Vector3sum(t.forward);
        float sidep = Vector3sum(Vector3.Project(connect, t.right)) / Vector3sum(t.right);
        float sidepp = Vector3sum(Vector3.Project(pplink, t.right)) / Vector3sum(t.right);
        if (forwardp > front || forwardp < -back || sidep > side || sidep < -side)
        {
            return Vector3.zero;
        }
        else if (((forwardp>0)&&(forwardp/front+Mathf.Abs(sidep/side)>1))||((forwardp<0)&&(-forwardp/back+Mathf.Abs(sidep/side)>1)))
        {
            return Vector3.zero;
        }
        else
        {
            Vector3 result = Vector3.zero;
            if (forwardp < front && forwardp > 0)
            {
                result += (1 - forwardp / front) * t.forward;
            }
            else if (forwardp > -back && forwardp < 0)
            {
                result -= (1 + forwardp / back) * t.forward;
            }
            if (sidep < side && sidep > 0)
            {
                result += (1 - sidep / side) * t.right;
            }
            else if (sidep > -side && sidep < 0)
            {
                result -= (1 + sidep / side) * t.right;
            }
            else
            {
                if (forwardpp > 0)
                {
                    result += t.forward;
                }
                else if (forwardpp < 0)
                {
                    result -= t.forward;
                }
                if (sidepp > 0)
                {
                    result += t.right;
                }
                else if (sidepp < 0)
                {
                    result -= t.right;
                }
            }
            return result;
        }
    }

    public Vector3 CDline(List<Vector3> points)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((points[1] - np).magnitude, 2)-Mathf.Pow(Vector3.Project(points[1]-np, points[1]-points[0]).magnitude, 2));
        Vector3 linesegment = points[1] - points[0];
        Vector3 vert = (Vector3.Cross(linesegment, t.up)).normalized;
        Vector3 connect = vert * distance;
        if (connect.y > 1)
            return Vector3.zero;
        float forwardp = Vector3sum(Vector3.Project(connect, t.forward)) / Vector3sum(t.forward);
        float sidep = Vector3sum(Vector3.Project(connect, t.right)) / Vector3sum(t.right);
        if (forwardp > front || forwardp < -back || sidep > side || sidep < -side)
        {
            return Vector3.zero;
        }
        else
        {
            float lr = 0, fb = 0;
            if (forwardp < front && forwardp > 0)
            {
                fb = 1 - forwardp / front;
            }
            else if (forwardp > -back && forwardp <= 0)
            {
                fb = 1 + forwardp / back;
            }
            if (sidep < side && sidep > 0)
            {
                lr = 1 - sidep / side;
            }
            else if (sidep > -side && sidep <= 0)
            {
                lr = 1 + sidep / side;
            }
            return Mathf.Sqrt(fb*fb + lr*lr)*vert;
        }
    }

    // public Vector3 CDs(Vector3 a, Vector3 b, int num, int op)
    public Vector3 CDs(List<Vector3> points, Vector3 cp, int op)
    {
        if (points.Count == 0)
            return Vector3.zero;
        if (points.Count == 1)
            return CD(points[0], cp);
        else
        {
            Vector3 result = Vector3.zero;
            //if (points.Count == 3)
            //    Debug.Log("3");
            switch (op)
            {
                case 1://average the reactions
                    {
                        Vector3 temp;
                        float i = 0;
                        float k;
                        foreach (Vector3 p in points)
                        {
                            temp = CD(p, cp);
                            if (temp != Vector3.zero)
                            {
                                k = Random.Range(1.0f, 99.0f);
                                result += k*temp;
                                i += k;
                            }
                        }
                        if (i > 0)
                        {
                            result /= i;
                        }
                        //Debug.Log(op + " " + result);
                        return result;
                    }
                case 2://average the contact points
                    {
                        Vector3 average = Vector3.zero;
                        foreach (Vector3 v in points)
                        {
                            average += v;
                        }
                        average /= points.Count;
                        result = CD(average, cp);
                        //Debug.Log(op + " " + result);
                        return result;
                    }
                case 3://the reaction to a line segment
                    {
                        result = CDline(points);
                        //Debug.Log(op + " " + result);
                        return result;
                    }
                default:
                    //Debug.Log("Nothing is applied.");
                    return Vector3.zero;
            }
        }
    }

    static float Vector3sum(Vector3 av)
    {
        return av.x + av.y + av.z;
    }
}

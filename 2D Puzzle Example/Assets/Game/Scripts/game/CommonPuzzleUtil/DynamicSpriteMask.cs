using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.CommonPuzzleUtil
{
    public class DynamicSpriteMask : MonoBehaviour
    {

        public SpriteMask Mask;//1048576
        Color[] Colors;
        int Width, Height;
        public Color drawColor;
        public Color undrawColor;
        public int range;
        void Start()
        {
            //Extract color data once
            Colors = Mask.sprite.texture.GetPixels();
            _appliedColorArray = new bool[Colors.Length];
            //Store mask dimensionns
            Width = Mask.sprite.texture.width;
            Height = Mask.sprite.texture.height;

            ClearMask();
        }

        void ClearMask()
        {
            //set all color data to transparent
            for (int i = 0; i < Colors.Length; ++i)
            {
                Colors[i] = new Color(1, 1, 1, 0);
            }

            Mask.sprite.texture.SetPixels(Colors);
            Mask.sprite.texture.Apply(false);
        }

        //This function will draw a circle onto the texture at position cx, cy with radius r
        public void DrawOnMask(int cx, int cy, int r, Color c)
        {
            if (cx >= Width || cy >= Height)
                return;
            if (cx < 0 || cy < 0)
                return;

            //Debug.Log("DrawOnMask " + cx + " " + cy + " " + r);
            int px, nx, py, ny, d;

            for (int x = 0; x <= r; x++)
            {
                d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

                for (int y = 0; y <= d; y++)
                {
                    px = cx + x;
                    nx = cx - x;
                    py = cy + y;
                    ny = cy - y;
                    if (px < Width && py < Height)
                        SetColors(py * Width + px, c);//1/4 round TR
                    if (nx >= 0 && py < Height)
                        SetColors(py * Width + nx, c);//1/4 round TL
                    if (px < Width && ny >= 0)
                        SetColors(ny * Height + px, c);//1/4 round BR
                    if (nx >= 0 && ny >= 0)
                        SetColors(ny * Height + nx, c);//1/4 round BL
                }
            }

            Mask.sprite.texture.SetPixels(Colors);
            Mask.sprite.texture.Apply(false);
        }

        bool[] _appliedColorArray;

        void SetColors(int i, Color c)
        {
            if (i >= Colors.Length || i < 0)
            {
                return;
            }
            if (_appliedColorArray[i])
            {
                return;
            }
            Colors[i] = c;
        }

        void Update()
        {

            //Get mouse coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Check if mouse button is held down
            if (Input.GetMouseButton(0))
            {
                //Check if we hit the collider
                // RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                //    if (hit.collider != null)
                //    {
                //Normalize to the texture coodinates
                int y = (int)((0.5 - (Mask.transform.position - mousePosition).y) * Height);
                int x = (int)((0.5 - (Mask.transform.position - mousePosition).x) * Width);

                //Draw onto the mask
                DrawOnMask(x, y, range, drawColor);
                //   }
            }
            if (Input.GetMouseButton(1))
            {
                int y = (int)((0.5 - (Mask.transform.position - mousePosition).y) * Height);
                int x = (int)((0.5 - (Mask.transform.position - mousePosition).x) * Width);
                DrawOnMask(x, y, range, undrawColor);
            }
        }
    }
}
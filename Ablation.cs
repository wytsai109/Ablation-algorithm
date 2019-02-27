for (i = 0; i < total_phase; i++)
            {
                for (j = 0; j < layers[i]; j++)
                {
                    r0 = mintable[i][j];
                    theta = asti_degree * Math.PI / 180.0;
                    radius = maxtable[i][j] - r0;
                    PP10.X = r0 * Math.Cos(theta) - radius * Math.Sin(theta);
                    PP10.Y = r0 * Math.Sin(theta) + radius * Math.Cos(theta);
                    PP11.X = r0 * Math.Cos(theta) + radius * Math.Sin(theta);
                    PP11.Y = r0 * Math.Sin(theta) - radius * Math.Cos(theta);
                    
                    P0.X = P0.Y = 0.0;
                    center_X_1 = r0 * Math.Cos(theta);
                    center_Y_1 = r0 * Math.Sin(theta);
                    PP20.X = -PP10.X;
                    PP20.Y = -PP10.Y;
                    PP21.X = -PP11.X;
                    PP21.Y = -PP11.Y;

                    g.FillEllipse(brush, new RectangleF((float)PP10.X * ratio_x - 2.5f, (float)-PP10.Y * ratio_y - 2.5f, 5, 5));
                    g.FillEllipse(brush, new RectangleF((float)PP11.X * ratio_x - 2.5f, (float)-PP11.Y * ratio_y - 2.5f, 5, 5)); 
                    for (k = 0; k < num_of_points; k++)
                    {
                        P1.X = point_XY_pos[k].X;
                        P1.Y = point_XY_pos[k].Y;
                        
                        if (intersection(P0, P1, PP10, PP11))
                        {
                            int ccw;
                            ccw=CCW(P0, P1, PP10) * CCW(P0, P1, PP11);
                            ccw=CCW(PP10, PP11, P1) * CCW(PP10, PP11, P1);

                            rad = Math.Sqrt((P1.X - center_X_1) * (P1.X - center_X_1) +
                               (P1.Y - center_Y_1) * (P1.Y - center_Y_1));
                            if (rad <= radius)
                            {
                                shots[i][k]++;
                                g.FillEllipse(brush, new RectangleF((float)P1.X * ratio_x - 2.5f, (float)-P1.Y * ratio_y - 2.5f, 5, 5));                                 
                            }
                        }
                        else if (intersection(P0, P1, PP20, PP21))
                        {
                            rad = Math.Sqrt((P1.X + center_X_1) * (P1.X + center_X_1) +
                              (P1.Y + center_Y_1) * (P1.Y + center_Y_1));
                            if (rad <= radius)
                            {
                               shots[i][k]++;
                               g.FillEllipse(brush, new RectangleF((float)P1.X * ratio_x - 2.5f, -(float)P1.Y * ratio_y - 2.5f, 5, 5));
                            }
                        }
                    }
                }
            }
      private static bool intersection(Point_Type p11, Point_Type p12,
               Point_Type p21, Point_Type p22)
        {
            return ((CCW( p11,   p12,   p21) * CCW( p11,   p12,   p22) <= 0) &&
               (CCW( p21,   p22,   p11) * CCW( p21,   p22,   p12) <= 0));
        }
        private static int CCW(Point_Type p0, Point_Type p1, Point_Type p2)
        {
            double dx1, dx2, dy1, dy2;

            dx1 = p1.X - p0.X;
            dy1 = p1.Y - p0.Y;
            dx2 = p2.X - p0.X;
            dy2 = p2.Y - p0.Y;
            if (dx1 * dy2 > dy1 * dx2) return (1);
            if (dx1 * dy2 < dy1 * dx2) return (-1);
            if (dx1 * dy2 == dy1 * dx2)
            {
                //if ((dx1 * dx2 < 0) || (dy1 * dy2)) return (-1);
                if ((dx1 * dx2 < 0)) return (-1);
                else if ((dx1 * dx1 + dy1 * dy1) >= (dx2 * dx2 + dy2 * dy2)) return (0);
                else return (1);
            }
            return (0);
        }    

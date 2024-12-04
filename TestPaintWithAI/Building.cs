using System;
using System.Drawing;

public class Building
{
    private int x, width, height;
    private int windowRows, windowCols;
    private bool[] windowLights;
    private static Random random = new Random();
    private enum BuildingType { Regular, Bank, Mosque }
    private BuildingType buildingType;
    private int columnCount;

    public Building(int x, int width, int height)
    {
        this.x = x;
        this.width = width;
        this.height = height;
        
        // 15% chance for bank, 10% for mosque
        int buildingChance = random.Next(100);
        if (buildingChance < 15)
            buildingType = BuildingType.Bank;
        else if (buildingChance < 25)
            buildingType = BuildingType.Mosque;
        else
            buildingType = BuildingType.Regular;

        switch (buildingType)
        {
            case BuildingType.Bank:
                this.columnCount = width / 25;
                this.windowRows = (height - 60) / 30;
                this.windowCols = columnCount - 1;
                break;
            case BuildingType.Mosque:
                this.windowRows = (height - 100) / 40; // Fewer, larger windows
                this.windowCols = width / 40;
                break;
            default:
                this.windowCols = width / 20;
                this.windowRows = height / 30;
                break;
        }

        windowLights = new bool[Math.Max(1, windowRows * windowCols)];
        for (int i = 0; i < windowLights.Length; i++)
        {
            windowLights[i] = random.Next(100) < 40;
        }
    }

    public void Draw(Graphics g, float groundY)
    {
        float buildingTop = groundY - height;

        switch (buildingType)
        {
            case BuildingType.Bank:
                DrawBankBuilding(g, buildingTop, groundY);
                break;
            case BuildingType.Mosque:
                DrawMosqueBuilding(g, buildingTop, groundY);
                break;
            default:
                DrawRegularBuilding(g, buildingTop);
                break;
        }
    }

    private void DrawMosqueBuilding(Graphics g, float buildingTop, float groundY)
    {
        // Main building structure
        using (SolidBrush buildingBrush = new SolidBrush(Color.FromArgb(255, 35, 35, 55)))
        {
            g.FillRectangle(buildingBrush, x, buildingTop, width, height);
        }

        // Draw dome
        int domeHeight = 60;
        int domeWidth = width * 2/3;
        int domeX = x + (width - domeWidth)/2;
        
        using (SolidBrush domeBrush = new SolidBrush(Color.FromArgb(255, 45, 45, 65)))
        {
            // Draw main dome
            Rectangle domeRect = new Rectangle(domeX, (int)buildingTop - domeHeight, domeWidth, domeHeight * 2);
            g.FillEllipse(domeBrush, domeRect);

            // Draw spire
            int spireWidth = 6;
            int spireHeight = 40;
            g.FillPolygon(domeBrush, new Point[] {
                new Point(domeX + domeWidth/2 - spireWidth/2, (int)buildingTop - domeHeight),
                new Point(domeX + domeWidth/2 + spireWidth/2, (int)buildingTop - domeHeight),
                new Point(domeX + domeWidth/2, (int)buildingTop - domeHeight - spireHeight)
            });

            // Draw crescent
            using (Pen crescentPen = new Pen(Color.FromArgb(255, 200, 200, 200), 2))
            {
                int crescentSize = 15;
                g.DrawArc(crescentPen, 
                    domeX + domeWidth/2 - crescentSize/2,
                    (int)buildingTop - domeHeight - spireHeight - crescentSize,
                    crescentSize, crescentSize, -30, 240);
            }
        }

        // Draw arched windows
        int windowWidth = 30;
        int windowHeight = 40;
        int archHeight = 15;
        int startY = (int)buildingTop + 40;
        int windowSpacingX = (width - (windowCols * windowWidth)) / (windowCols + 1);
        int windowSpacingY = (height - 100 - windowHeight) / (windowRows + 1);

        for (int row = 0; row < windowRows; row++)
        {
            for (int col = 0; col < windowCols; col++)
            {
                int windowX = x + windowSpacingX + (col * (windowWidth + windowSpacingX));
                int windowY = startY + (row * windowSpacingY);
                
                Color windowColor = windowLights[row * windowCols + col] 
                    ? Color.FromArgb(255, 255, 255, 200)
                    : Color.FromArgb(255, 40, 40, 60);

                using (SolidBrush windowBrush = new SolidBrush(windowColor))
                {
                    // Draw rectangular part
                    g.FillRectangle(windowBrush, windowX, windowY + archHeight, windowWidth, windowHeight - archHeight);
                    
                    // Draw arch
                    Rectangle archRect = new Rectangle(windowX, windowY, windowWidth, archHeight * 2);
                    g.FillEllipse(windowBrush, archRect);
                }
            }
        }

        // Draw decorative patterns
        using (Pen patternPen = new Pen(Color.FromArgb(255, 200, 200, 200), 1))
        {
            int patternHeight = 20;
            int patternY = (int)groundY - patternHeight;
            int patternSpacing = width / 10;
            
            for (int i = 0; i <= 10; i++)
            {
                int patternX = x + (i * patternSpacing);
                g.DrawArc(patternPen, patternX - patternSpacing/4, patternY, 
                         patternSpacing/2, patternHeight, 0, 180);
            }
        }
    }

    private void DrawBankBuilding(Graphics g, float buildingTop, float groundY)
    {
        // Draw main building structure
        using (SolidBrush buildingBrush = new SolidBrush(Color.FromArgb(255, 30, 30, 50)))
        {
            g.FillRectangle(buildingBrush, x, buildingTop, width, height);
        }

        // Draw classical top
        int roofHeight = 20;
        using (SolidBrush roofBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 60)))
        {
            g.FillPolygon(roofBrush, new Point[] {
                new Point(x - 10, (int)buildingTop),
                new Point(x + width + 10, (int)buildingTop),
                new Point(x + width, (int)buildingTop + roofHeight),
                new Point(x, (int)buildingTop + roofHeight)
            });
        }

        // Draw columns
        int columnWidth = 20;
        int columnSpacing = width / (columnCount + 1);
        int columnHeight = 60;
        
        for (int i = 0; i < columnCount; i++)
        {
            int columnX = x + columnSpacing * (i + 1) - columnWidth/2;
            int columnY = (int)groundY - columnHeight;
            
            // Draw column
            using (SolidBrush columnBrush = new SolidBrush(Color.FromArgb(255, 200, 200, 200)))
            {
                g.FillRectangle(columnBrush, columnX, columnY, columnWidth, columnHeight);
                
                // Draw column capital
                g.FillRectangle(columnBrush, columnX - 5, columnY, columnWidth + 10, 10);
                g.FillRectangle(columnBrush, columnX - 5, columnY + columnHeight - 10, columnWidth + 10, 10);
            }
        }

        // Draw windows
        int windowWidth = 15;
        int windowHeight = 25;
        int startY = (int)buildingTop + roofHeight + 20;
        int windowSpacingX = columnSpacing - windowWidth;
        int windowSpacingY = (height - columnHeight - roofHeight - 40) / (windowRows + 1);

        for (int row = 0; row < windowRows; row++)
        {
            for (int col = 0; col < windowCols; col++)
            {
                int windowX = x + columnSpacing + (col * columnSpacing) - windowWidth/2;
                int windowY = startY + (row * windowSpacingY);
                
                Color windowColor = windowLights[row * windowCols + col] 
                    ? Color.FromArgb(255, 255, 255, 200)
                    : Color.FromArgb(255, 40, 40, 60);

                using (SolidBrush windowBrush = new SolidBrush(windowColor))
                {
                    g.FillRectangle(windowBrush, windowX, windowY, windowWidth, windowHeight);
                }
            }
        }
    }

    private void DrawRegularBuilding(Graphics g, float buildingTop)
    {
        // Original regular building drawing code
        using (SolidBrush buildingBrush = new SolidBrush(Color.FromArgb(255, 20, 20, 40)))
        {
            g.FillRectangle(buildingBrush, x, buildingTop, width, height);
        }

        int windowWidth = 10;
        int windowHeight = 15;
        int windowSpacingX = (width - (windowCols * windowWidth)) / (windowCols + 1);
        int windowSpacingY = (height - (windowRows * windowHeight)) / (windowRows + 1);

        for (int row = 0; row < windowRows; row++)
        {
            for (int col = 0; col < windowCols; col++)
            {
                int windowX = x + windowSpacingX + (col * (windowWidth + windowSpacingX));
                int windowY = (int)buildingTop + windowSpacingY + (row * (windowHeight + windowSpacingY));
                
                Color windowColor = windowLights[row * windowCols + col] 
                    ? Color.FromArgb(255, 255, 255, 200)
                    : Color.FromArgb(255, 40, 40, 60);

                using (SolidBrush windowBrush = new SolidBrush(windowColor))
                {
                    g.FillRectangle(windowBrush, windowX, windowY, windowWidth, windowHeight);
                }
            }
        }
    }

    public void Update()
    {
        if (random.Next(100) < 5)
        {
            int windowIndex = random.Next(windowLights.Length);
            windowLights[windowIndex] = !windowLights[windowIndex];
        }
    }
} 
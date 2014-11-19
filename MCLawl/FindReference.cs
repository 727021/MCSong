﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCLawl
{
    public static class FindReference
    {
        public static ushort writeLetter(Player p, char c, ushort x, ushort y, ushort z, byte b, int direction)
        {
            return writeLetter(p.level, p, c, x, y, z, b, direction);
        }

        public static ushort writeLetter(Level l, Player p, char c, ushort x, ushort y, ushort z, byte b, int directionToGo)
        {
            switch (c)
            {
                case 'A':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'B':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'C':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'D':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'E':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'F':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'G':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'H':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'I':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'J':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'K':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'L':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    break;
                case 'M':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'N':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'O':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'P':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'Q':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'R':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case 'S':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'T':
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'U':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'V':
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'W':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'X':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'Y':
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case 'Z':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;


                case '0':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case '1':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    break;
                case '2':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '3':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '4':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    break;
                case '5':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '6':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '7':
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '8':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '9':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;


                case ':':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case '\\':
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    break;
                case '/':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case '.':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    break;
                case '!':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    break;
                case ',':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    break;
                case '\'':
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case '"':
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 4), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;
                case ' ':
                    break;


                case '+':
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    break;
                case '-':
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 2), z, b);
                    break;
                case '_':
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 0), z, b);
                    break;
                case '=':
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    placeBlock(l, p, x, (ushort)(y + 1), z, b);
                    placeBlock(l, p, x, (ushort)(y + 3), z, b);
                    break;

                default:
                    Player.SendMessage(p, "\"" + c + "\" is currently not supported. Space left");
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    if (directionToGo == 0) x++; else if (directionToGo == 1) x--; else if (directionToGo == 2) z++; else z--;
                    break;
            }

            if (directionToGo == 0) return (ushort)(x + 2);
            else if (directionToGo == 1) return (ushort)(x - 2);
            else if (directionToGo == 2) return (ushort)(z + 2);
            else return (ushort)(z - 2);
        }

        public static List<ColorBlock> popRefCol(byte popType)
        {
            ColorBlock tempref = new ColorBlock();
            List<ColorBlock> refCol = new List<ColorBlock>();
            refCol.Clear();
            //FRONT LAYER BLOCKS
            if (popType == 1)   //poptype 1 = 2-layer color image
            {
                //FRONT LAYER BLOCKS
                tempref.r = 128;
                tempref.g = 86;
                tempref.b = 57;
                tempref.type = 3;
                refCol.Add(tempref);
                tempref.r = 162;
                tempref.g = 129;
                tempref.b = 75;
                tempref.type = 5;
                refCol.Add(tempref);
                tempref.r = 244;
                tempref.g = 237;
                tempref.b = 174;
                tempref.type = 12;
                refCol.Add(tempref);
                tempref.r = 226;
                tempref.g = 31;
                tempref.b = 38;
                tempref.type = 21;
                refCol.Add(tempref);
                tempref.r = 223;
                tempref.g = 135;
                tempref.b = 37;
                tempref.type = 22;
                refCol.Add(tempref);
                tempref.r = 230;
                tempref.g = 241;
                tempref.b = 25;
                tempref.type = 23;
                refCol.Add(tempref);
                tempref.r = 127;
                tempref.g = 234;
                tempref.b = 26;
                tempref.type = 24;
                refCol.Add(tempref);
                tempref.r = 25;
                tempref.g = 234;
                tempref.b = 20;
                tempref.type = 25;
                refCol.Add(tempref);
                tempref.r = 31;
                tempref.g = 234;
                tempref.b = 122;
                tempref.type = 26;
                refCol.Add(tempref);
                tempref.r = 27;
                tempref.g = 239;
                tempref.b = 225;
                tempref.type = 27;
                refCol.Add(tempref);
                tempref.r = 99;
                tempref.g = 166;
                tempref.b = 226;
                tempref.type = 28;
                refCol.Add(tempref);
                tempref.r = 111;
                tempref.g = 124;
                tempref.b = 235;
                tempref.type = 29;
                refCol.Add(tempref);
                tempref.r = 126;
                tempref.g = 34;
                tempref.b = 218;
                tempref.type = 30;
                refCol.Add(tempref);
                tempref.r = 170;
                tempref.g = 71;
                tempref.b = 219;
                tempref.type = 31;
                refCol.Add(tempref);
                tempref.r = 227;
                tempref.g = 39;
                tempref.b = 225;
                tempref.type = 32;
                refCol.Add(tempref);
                tempref.r = 234;
                tempref.g = 39;
                tempref.b = 121;
                tempref.type = 33;
                refCol.Add(tempref);
                tempref.r = 46;
                tempref.g = 68;
                tempref.b = 47;
                tempref.type = 34;
                refCol.Add(tempref);
                tempref.r = 135;
                tempref.g = 145;
                tempref.b = 130;
                tempref.type = 35;
                refCol.Add(tempref);
                tempref.r = 230;
                tempref.g = 240;
                tempref.b = 225;
                tempref.type = 36;
                refCol.Add(tempref);

                // BACK LAYER BLOCKS

                tempref.r = 57;
                tempref.g = 38;
                tempref.b = 25;
                tempref.type = 3;
                refCol.Add(tempref);
                tempref.r = 72;
                tempref.g = 57;
                tempref.b = 33;
                tempref.type = 5;
                refCol.Add(tempref);
                tempref.r = 109;
                tempref.g = 105;
                tempref.b = 77;
                tempref.type = 12;
                refCol.Add(tempref);
                tempref.r = 41;
                tempref.g = 31;
                tempref.b = 16;
                tempref.type = 17;
                refCol.Add(tempref);
                tempref.r = 101;
                tempref.g = 13;
                tempref.b = 16;
                tempref.type = 21;
                refCol.Add(tempref);
                tempref.r = 99;
                tempref.g = 60;
                tempref.b = 16;
                tempref.type = 22;
                refCol.Add(tempref);
                tempref.r = 102;
                tempref.g = 107;
                tempref.b = 11;
                tempref.type = 23;
                refCol.Add(tempref);
                tempref.r = 56;
                tempref.g = 104;
                tempref.b = 11;
                tempref.type = 24;
                refCol.Add(tempref);
                tempref.r = 11;
                tempref.g = 104;
                tempref.b = 8;
                tempref.type = 25;
                refCol.Add(tempref);
                tempref.r = 13;
                tempref.g = 104;
                tempref.b = 54;
                tempref.type = 26;
                refCol.Add(tempref);
                tempref.r = 12;
                tempref.g = 106;
                tempref.b = 100;
                tempref.type = 27;
                refCol.Add(tempref);
                tempref.r = 44;
                tempref.g = 74;
                tempref.b = 101;
                tempref.type = 28;
                refCol.Add(tempref);
                tempref.r = 49;
                tempref.g = 55;
                tempref.b = 105;
                tempref.type = 29;
                refCol.Add(tempref);
                tempref.r = 56;
                tempref.g = 15;
                tempref.b = 97;
                tempref.type = 30;
                refCol.Add(tempref);
                tempref.r = 75;
                tempref.g = 31;
                tempref.b = 97;
                tempref.type = 31;
                refCol.Add(tempref);
                tempref.r = 101;
                tempref.g = 17;
                tempref.b = 100;
                tempref.type = 32;
                refCol.Add(tempref);
                tempref.r = 104;
                tempref.g = 17;
                tempref.b = 54;
                tempref.type = 33;
                refCol.Add(tempref);
                tempref.r = 20;
                tempref.g = 30;
                tempref.b = 21;
                tempref.type = 34;
                refCol.Add(tempref);
                tempref.r = 60;
                tempref.g = 64;
                tempref.b = 58;
                tempref.type = 35;
                refCol.Add(tempref);
                tempref.r = 102;
                tempref.g = 107;
                tempref.b = 100;
                tempref.type = 36;
                refCol.Add(tempref);
                tempref.r = 0;
                tempref.g = 0;
                tempref.b = 0;
                tempref.type = 49;
                refCol.Add(tempref);
            }
            else if (popType == 2) // poptype 2 = 1 layer color image
            {
                tempref.r = 128;
                tempref.g = 86;
                tempref.b = 57;
                tempref.type = 3;
                refCol.Add(tempref);
                tempref.r = 162;
                tempref.g = 129;
                tempref.b = 75;
                tempref.type = 5;
                refCol.Add(tempref);
                tempref.r = 244;
                tempref.g = 237;
                tempref.b = 174;
                tempref.type = 12;
                refCol.Add(tempref);
                tempref.r = 93;
                tempref.g = 70;
                tempref.b = 38;
                tempref.type = 17;
                refCol.Add(tempref);
                tempref.r = 226;
                tempref.g = 31;
                tempref.b = 38;
                tempref.type = 21;
                refCol.Add(tempref);
                tempref.r = 223;
                tempref.g = 135;
                tempref.b = 37;
                tempref.type = 22;
                refCol.Add(tempref);
                tempref.r = 230;
                tempref.g = 241;
                tempref.b = 25;
                tempref.type = 23;
                refCol.Add(tempref);
                tempref.r = 127;
                tempref.g = 234;
                tempref.b = 26;
                tempref.type = 24;
                refCol.Add(tempref);
                tempref.r = 25;
                tempref.g = 234;
                tempref.b = 20;
                tempref.type = 25;
                refCol.Add(tempref);
                tempref.r = 31;
                tempref.g = 234;
                tempref.b = 122;
                tempref.type = 26;
                refCol.Add(tempref);
                tempref.r = 27;
                tempref.g = 239;
                tempref.b = 225;
                tempref.type = 27;
                refCol.Add(tempref);
                tempref.r = 99;
                tempref.g = 166;
                tempref.b = 226;
                tempref.type = 28;
                refCol.Add(tempref);
                tempref.r = 111;
                tempref.g = 124;
                tempref.b = 235;
                tempref.type = 29;
                refCol.Add(tempref);
                tempref.r = 126;
                tempref.g = 34;
                tempref.b = 218;
                tempref.type = 30;
                refCol.Add(tempref);
                tempref.r = 170;
                tempref.g = 71;
                tempref.b = 219;
                tempref.type = 31;
                refCol.Add(tempref);
                tempref.r = 227;
                tempref.g = 39;
                tempref.b = 225;
                tempref.type = 32;
                refCol.Add(tempref);
                tempref.r = 234;
                tempref.g = 39;
                tempref.b = 121;
                tempref.type = 33;
                refCol.Add(tempref);
                tempref.r = 46;
                tempref.g = 68;
                tempref.b = 47;
                tempref.type = 34;
                refCol.Add(tempref);
                tempref.r = 135;
                tempref.g = 145;
                tempref.b = 130;
                tempref.type = 35;
                refCol.Add(tempref);
                tempref.r = 230;
                tempref.g = 240;
                tempref.b = 225;
                tempref.type = 36;
                refCol.Add(tempref);
                tempref.r = 0;
                tempref.g = 0;
                tempref.b = 0;
                tempref.type = 49;
                refCol.Add(tempref);
            }
            else if (popType == 3) //2-Layer Gray Scale
            {
                //FRONT LAYER
                tempref.r = 46;
                tempref.g = 68;
                tempref.b = 47;
                tempref.type = 34;
                refCol.Add(tempref);
                tempref.r = 135;
                tempref.g = 145;
                tempref.b = 130;
                tempref.type = 35;
                refCol.Add(tempref);
                tempref.r = 230;
                tempref.g = 240;
                tempref.b = 225;
                tempref.type = 36;
                refCol.Add(tempref);
                //BACK LAYER
                tempref.r = 20;
                tempref.g = 30;
                tempref.b = 21;
                tempref.type = 34;
                refCol.Add(tempref);
                tempref.r = 60;
                tempref.g = 64;
                tempref.b = 58;
                tempref.type = 35;
                refCol.Add(tempref);
                tempref.r = 102;
                tempref.g = 107;
                tempref.b = 100;
                tempref.type = 36;
                refCol.Add(tempref);
                tempref.r = 0;
                tempref.g = 0;
                tempref.b = 0;
                tempref.type = 49;
                refCol.Add(tempref);
            }
            else if (popType == 4) //1-Layer grayscale
            {
                tempref.r = 46;
                tempref.g = 68;
                tempref.b = 47;
                tempref.type = 34;
                refCol.Add(tempref);
                tempref.r = 135;
                tempref.g = 145;
                tempref.b = 130;
                tempref.type = 35;
                refCol.Add(tempref);
                tempref.r = 230;
                tempref.g = 240;
                tempref.b = 225;
                tempref.type = 36;
                refCol.Add(tempref);
                tempref.r = 0;
                tempref.g = 0;
                tempref.b = 0;
                tempref.type = 49;
                refCol.Add(tempref);
            }
            else if (popType == 5) // Black and white 1 layer
            {
                tempref.r = 255;
                tempref.g = 255;
                tempref.b = 255;
                tempref.type = 36;
                refCol.Add(tempref);
                tempref.r = 0;
                tempref.g = 0;
                tempref.b = 0;
                tempref.type = 49;
                refCol.Add(tempref);
            }
            return refCol;
        }

        public static void placeBlock(Level l, Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (p == null)
                l.Blockchange(x, y, z, type);
            else
                l.Blockchange(p, x, y, z, type);
        }

        public struct ColorBlock
        {
            public ushort x, y, z; public byte type, r, g, b, a;
        }
    }
}

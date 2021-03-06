﻿using UnityEngine;
using System.Collections;

public enum ITEM_ORDER
{
    ITEM_ORDER_FLOOR = -1000000,
    ITEM_ORDER_BACK = 0,
    ITEM_ORDER_CHARACTER = 10,
    ITEM_ORDER_FRONT = 20,
    ITEM_ORDER_CELL = 1000000
}

public enum RESOURCE_TYPE
{
    RESOURCE_PREFAB = 0,
    RESOURCE_TEXTURE,
    RESOURCE_TEXTASSET
}

public enum CHARACTER_RANK  //temp
{
    NORMAL = 0,
    GOOD,
    RARE,
    LEGENDARY
}

public enum CHARACTER_TYPE
{
    ACTOR = 0,
    VOICE_ACTOR,
    TICHNICHAN
}

public enum MOVIE_TYPE
{
    FILM = 0,
    CARTOON,
    TV_SERIAL
}

public enum MOVIE_STAGE
{
    NONE = 0,
    STAGE_1,
    STAGE_2,
    STAGE_3,
    STAGE_4,
    STAGE_5
}

public enum COMPANY_STAGE
{
    NONE = 0,
    STAGE_1,
    STAGE_2,
    STAGE_3,
    STAGE_4,
    STAGE_5
}

public class GameConstant {
    static public int DOOR_DIR_NONE = 0;
    static public int DOOR_DIR_TOPLEFT = 1;
    static public int DOOR_DIR_TOPRIGHT = 2;
    static public int DOOR_DIR_BOTLEFT = 4;
    static public int DOOR_DIR_BOTRIGHT = 8;

    static public int ITEM_ORDER_UPLIMIT = 30;
}

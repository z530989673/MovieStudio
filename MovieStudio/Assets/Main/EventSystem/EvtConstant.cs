﻿
public enum EVT_TYPE
{
    EVT_TYPE_DEFAULT = 0,
    EVT_TYPE_ENTER_GAME,
    EVT_TYPE_CHANGE_SCREEN,
	
	EVT_TYPE_MAKE_MOVIE,
	EVT_TYPE_MAKING_MOVIE,
	EVT_TYPE_AFTERMAKING_MOVIE,
	EVT_TYPE_FINISHMAKING_MOVIE,

    EVT_TYPE_PRELOAD_PARTIAL_FINISH,
    EVT_TYPE_PRELOAD_TOTAL_FINISH,
    EVT_TYPE_LOAD_FAILED,

    EVT_TYPE_MAX,
}

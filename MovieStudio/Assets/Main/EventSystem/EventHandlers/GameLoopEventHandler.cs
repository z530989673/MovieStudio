using UnityEngine;
using System.Collections;

public class GameLoopEventHandler{
    public static void EnterGame(Event evt)
    {
        UIManager.Instance.OpenScreen("MainScreen");
        UIManager.Instance.SetOverlayEnable("TopBar", true);
    }


    public static void MakeMovie(Event evt)
    {

        GameObject newMovie = UIManager.Instance.LoadPopUp("NewMoviePopUp");
        UIManager.Instance.SetPopupEnable("NewMoviePopUp", !newMovie.activeInHierarchy);
    }

    public static void MakingMovie(Event evt)
    {
        UIManager.Instance.LoadPopUp("NewMoviePopUp");
        UIManager.Instance.SetPopupEnable("NewMoviePopUp", false);

        UIManager.Instance.SetOverlayEnable("MovieMaking", true);
        MovieMakingController mmc = UIManager.Instance.GetOverlay("MovieMaking").GetComponent<MovieMakingController>();
        mmc.movieStatus = MovieMakingController.MovieStatus.Making;
    }

    public static void AfterEffectStart(Event evt)
    {
        UIManager.Instance.SetOverlayEnable("MovieMaking", false);

        UIManager.Instance.LoadPopUp("AfterEffectPopUp");
        UIManager.Instance.SetPopupEnable("AfterEffectPopUp", true);
    }

    public static void AfterEffectWorkerChoosen(Event evt)
    {
        UIManager.Instance.SetPopupEnable("AfterEffectPopUp", false);
        UIManager.Instance.SetOverlayEnable("MovieMaking", true);
        MovieMakingController mmc = UIManager.Instance.GetOverlay("MovieMaking").GetComponent<MovieMakingController>();
        mmc.movieStatus = MovieMakingController.MovieStatus.AfterEffect;
    }

    public static void MovieDone(Event evt)
    {
        UIManager.Instance.SetOverlayEnable("MovieMaking", false);
        UIManager.Instance.LoadPopUp("MovieDonePopUp");
        UIManager.Instance.SetPopupEnable("MovieDonePopUp", true);
    }

    public static void PublishMovie(Event evt)
    {
        UIManager.Instance.SetPopupEnable("MovieDonePopUp", false);
    }

    public static void AddAdvertisement(Event evt)
    {
        Debug.Log("doing nothing right now...");
    }
}

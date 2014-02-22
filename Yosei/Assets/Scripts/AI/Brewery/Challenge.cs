using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Challenge
{
    private event ChallengeCompleteHandler _challenge_complete;
    protected List<Yosei> _yosei_team;

    public delegate void ChallengeCompleteHandler(ChallengeCompleteInfo p_info);

    public class ChallengeCompleteInfo : EventArgs
    {
        public DateTime Time
        {
            get;
            private set;
        }

        public float Score
        {
            get;
            private set;
        }

        public List<Yosei> Team
        {
            get;
            private set;
        }

        public ChallengeCompleteInfo(float p_score, List<Yosei> p_team)
        {
            Score = p_score;
            Time = DateTime.Now;
            Team = p_team;
        }
    }

    /// <summary>
    /// Notifies the Competition that the Challenge has been completed by the team
    /// </summary>
    /// <param name="p_score">A rating of the team's performance, used in some Competitions</param>
    protected void CompleteChallenge(float p_score)
    {
        if (_challenge_complete != null)
        {
            _challenge_complete(new ChallengeCompleteInfo(p_score, _yosei_team));
        }
    }

    /// <summary>
    /// Adds a handler to the Challenge Complete event
    /// Triggered when the Challenge is completed by the team
    /// </summary>
    /// <param name="p_handler">The Challenge Complete handler to subscribe to the event</param>
    public void SubscribeComplete(ChallengeCompleteHandler p_handler)
    {
        _challenge_complete += p_handler;
    }

    /// <summary>
    /// Unsubscribe from the Challenge Complete event
    /// </summary>
    /// <param name="p_handler">The Challenge Complete handler to unregister to the event</param>
    public void UnsubscribeComplete(ChallengeCompleteHandler p_handler)
    {
        _challenge_complete -= p_handler;
    }
}

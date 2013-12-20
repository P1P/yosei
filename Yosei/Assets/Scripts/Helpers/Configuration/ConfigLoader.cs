using UnityEngine;
using System.Collections;

public class ConfigLoader : MonoBehaviour
{
    public string m_filename;

    private ConfigurationFetcher m_fetcher;

    public void Start()
    {
        m_fetcher = new ConfigurationFetcher(m_filename);

        string section = "Endurance";

        Game.Inst.m_player.m_endurance.m_pick_cost = m_fetcher.GetFloat(section, "Pick_Cost");
        Game.Inst.m_player.m_endurance.m_decay_rate = m_fetcher.GetFloat(section, "Decay_Rate");
        Game.Inst.m_player.m_endurance.m_recharge_rate = m_fetcher.GetFloat(section, "Recharge_Rate");

        Game.Inst.m_player.m_endurance.m_time_kickin_recharge = m_fetcher.GetFloat(section, "Kickin_Recharge_Time");
        Game.Inst.m_player.m_endurance.m_kickin_recharge_value = m_fetcher.GetFloat(section, "Kickin_Recharge_Bonus");

        section = "Focus";

        Game.Inst.m_player.m_focus.m_step_per_stage = m_fetcher.GetInt(section, "Number_Hits_Required_Per_Stage");
        Game.Inst.m_player.m_focus.m_step_per_stage_increase_per_stage = m_fetcher.GetInt(section, "Number_Hits_Required_Per_Stage_Increase_Per_Stage");
        Game.Inst.m_player.m_focus.m_stamina_cost_reduction_per_stage = m_fetcher.GetFloat(section, "Stamina_Cost_Reduction_Per_Stage");
        Game.Inst.m_player.m_focus.m_stamina_bonus_per_stage = m_fetcher.GetFloat(section, "Stamina_Bonus_Per_Stage");
        Game.Inst.m_player.m_focus.m_time_between_hits = m_fetcher.GetFloat(section, "Time_Between_Hits");

        section = "Backpack";

        Game.Inst.m_player.m_backpack.m_max_drinks = m_fetcher.GetInt(section, "Number_Drinks");
        Game.Inst.m_player.m_backpack.m_drink_bonus = m_fetcher.GetFloat(section, "Drink_Bonus");

        section = "Controller";

        // 1: Rift with Gamepad
        // 2: Rift with Mouse/Keyboard
        // 3: Full Gamepad
        // 4: Full Mouse/Keyboard
        int scheme = m_fetcher.GetInt(section, "Scheme");
        Game.Inst.m_player.m_head_control_scheme = (
                    scheme == 0 ? Climber.HEAD_CONTROL_SCHEME.HEADTRACKING :
                    scheme == 1 ? Climber.HEAD_CONTROL_SCHEME.HEADTRACKING :
                    scheme == 2 ? Climber.HEAD_CONTROL_SCHEME.GAMEPAD :
                    scheme == 3 ? Climber.HEAD_CONTROL_SCHEME.MOUSE : default(Climber.HEAD_CONTROL_SCHEME));
        Game.Inst.m_player.m_muscle_control_scheme = (
                    scheme == 0 ? Climber.MUSCLE_CONTROL_SCHEME.GAMEPAD :
                    scheme == 1 ? Climber.MUSCLE_CONTROL_SCHEME.KEYBOARD :
                    scheme == 2 ? Climber.MUSCLE_CONTROL_SCHEME.GAMEPAD :
                    scheme == 3 ? Climber.MUSCLE_CONTROL_SCHEME.KEYBOARD : default(Climber.MUSCLE_CONTROL_SCHEME));
        Game.Inst.m_player.m_hand_control_scheme = (
                    scheme == 0 ? Climber.HAND_CONTROL_SCHEME.GAMEPAD :
                    scheme == 1 ? Climber.HAND_CONTROL_SCHEME.MOUSE :
                    scheme == 2 ? Climber.HAND_CONTROL_SCHEME.GAMEPAD :
                    scheme == 3 ? Climber.HAND_CONTROL_SCHEME.MOUSE : default(Climber.HAND_CONTROL_SCHEME));

        Game.Inst.m_player.InitializeInputs();

        section = "Audio";

        SoundBearer.m_static_enabled = m_fetcher.GetBool(section, "Enabled");
        SoundBearer.m_static_binaural = m_fetcher.GetBool(section, "Binaural");
    }
}

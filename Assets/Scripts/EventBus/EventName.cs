using System;

namespace EventBus
{
    [Serializable]
    public enum EventName
    {
        //CHARACTER SKIN CHANGE
        ON_CHARACTER_SKIN_CHANGE,
        ON_SKIN_IN_SHOP_SELECTED,
        ON_SKIN_IN_SHOP_PURCHASED,
        //INITIALIZATION
        ON_PLAYER_MODEL_CREATED,
        ON_MAP_OPENED,
        //LEVEL STATE CHANGE
        ON_LEVEL_STARTED,
        ON_LEVEL_ENDED,
        ON_LEVEL_WON,
        ON_LEVEL_LOST,
        //ADS
        ON_REWARDED_OPENED,
        ON_REWARDED_WATCHED,
        ON_REWARDED_SKIPPED,
    }
}


using Characters.Skins;
using Components.Music.Data;
using Level;
using SkinShop;
using UnityEditor;

[CustomPropertyDrawer(typeof(SkinItemTypeCollectionDictionary))]
internal class SkinItemTypeCollectionDictionaryUI : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(TrackTypeDictionary))]
internal class TrackTypeDictionaryUI : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(SkinItemTypeShopCollectionDictionary))]
internal class SkinItemTypeShopCollectionDictionaryUI : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(FractionRegionDictionary))]
internal class FractionRegionDictionaryUI : SerializableDictionaryPropertyDrawer {}
mergeInto(LibraryManager.library, {
    SaveExternal: function(fieldName, data) {
        var dataString = UTF8ToString(data);
        localStorage.setItem('our-land-data/' + UTF8ToString(fieldName), dataString);
    },
    GetSerializedExternal: function(fieldName) {
       SendMessage("Save", "GetSerializedData", localStorage.getItem('our-land-data/' + UTF8ToString(fieldName)) || '');
    },
    
    ShowAdExternal: function() {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                  // some action after close
                },
                onError: function(error) {
                  // some action on error
                }
            }
        })
    },
    
    ShowRewardedExternal: function() {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                  console.log("Rewarded Open");
                },
                onRewarded: () => {
                  SendMessage("Ads", "InvokeRewardedWatched");
                },
                onClose: () => {
                  SendMessage("Ads", "InvokeRewardedSkipped");
                }, 
                onError: (e) => {
                  console.log('Error while open video ad:', e);
                }
            }
        })
    }
});
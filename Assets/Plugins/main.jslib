mergeInto(LibraryManager.library, {
    SaveExternal: function(fieldName, data) {
        var dataString = UTF8ToString(data);
        localStorage.setItem('our-land-data/' + UTF8ToString(fieldName), dataString);
    },
    GetSerializedExternal: function(fieldName) {
       SendMessage("Save", "GetSerializedData", localStorage.getItem('our-land-data/' + UTF8ToString(fieldName)) || '');
    },
});
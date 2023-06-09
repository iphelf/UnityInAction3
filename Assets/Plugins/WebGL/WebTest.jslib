mergeInto(LibraryManager.library, {
  JsAlert: function(message) {
	window.alert(UTF8ToString(message));
  },
});
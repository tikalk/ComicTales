var ComicTales;
(function (ComicTales) {
    function Init(storyId) {
        var viewModel = new EditorViewModel(storyId);

        ko.applyBindings(viewModel);
    }
    ComicTales.Init = Init;

    var EditorViewModel = (function () {
        function EditorViewModel(storyId) {
            this.storyId = storyId;
            this.tiles = ko.observableArray([]);
            this.isLoading = ko.observable(false);
            this.hasUpdates = ko.observable(false);
            // load data
            this.loadTiles();

            // Start the connection
            this.initConnection();
        }
        EditorViewModel.prototype.refresh = function () {
            this.loadTiles();
        };

        EditorViewModel.prototype.addNewTile = function () {
            var _this = this;
            new ComicTales.EditTileDialog(function (tile) {
                _this.saveTile(tile);
            }).open();
        };

        EditorViewModel.prototype.editTile = function (tile) {
            new ComicTales.EditTileDialog(function (t) {
            }, tile).open();
        };

        EditorViewModel.prototype.deleteTile = function (tile) {
        };

        EditorViewModel.prototype.saveTile = function (tile) {
        };

        EditorViewModel.prototype.loadTiles = function () {
            var _this = this;
            this.isLoading(true);
            $.get('/Story/' + this.storyId + '/GetTiles', function (data, textStatus, jqXHR) {
                _this.isLoading(false);
                _this.updateTiles(data);
            });
        };

        EditorViewModel.prototype.updateTiles = function (data) {
            var _this = this;
            this.tiles.removeAll();
            $.each(data.tiles, function (index, tile) {
                return _this.tiles.push(tile);
            });
        };

        EditorViewModel.prototype.initConnection = function () {
            var _this = this;
            // Proxy created on the fly
            var storyNotifications = $.connection.storyNotifications;

            // Declare a function on the chat hub so the server can invoke it
            storyNotifications.client.notifyHasUpdates = function () {
                console.log('Recieve updates notification!');
                _this.hasUpdates(true);
            };

            // Start the connection
            $.connection.hub.start(function () {
                return storyNotifications.server.join(_this.storyId);
            }).done(function () {
                console.log('Now connected, connection ID=' + $.connection.hub.id + ', transport=' + $.connection.hub.transport.name);
            }).fail(function () {
                console.log('Could not Connect!');
            });
        };
        return EditorViewModel;
    })();
    ComicTales.EditorViewModel = EditorViewModel;
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=Editor.js.map

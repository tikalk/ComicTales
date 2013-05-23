/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />
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
            this.loadTiles();
        }
        EditorViewModel.prototype.refresh = function () {
            this.loadTiles();
        };
        EditorViewModel.prototype.addNewTile = function () {
            new ComicTales.EditTileDialog().open();
        };
        EditorViewModel.prototype.editTile = function (tile) {
            new ComicTales.EditTileDialog(tile).open();
        };
        EditorViewModel.prototype.deleteTile = function (tile) {
            // todo: not implemented yet
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
        return EditorViewModel;
    })();
    ComicTales.EditorViewModel = EditorViewModel;    
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=Editor.js.map

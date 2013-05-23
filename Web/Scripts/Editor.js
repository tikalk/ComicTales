/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />
var ComicTales;
(function (ComicTales) {
    function Init() {
        var viewModel = new EditorViewModel();
        ko.applyBindings(viewModel);
    }
    ComicTales.Init = Init;
    var EditorViewModel = (function () {
        function EditorViewModel() {
            this.tiles = ko.observableArray([]);
            this.tiles = ko.observableArray([
                new TileViewModel('aaa')
            ]);
        }
        EditorViewModel.prototype.addNewTile = function () {
            new ComicTales.EditTileDialog().open();
        };
        EditorViewModel.prototype.editTile = function (tile) {
            new ComicTales.EditTileDialog(tile).open();
        };
        EditorViewModel.prototype.deleteTile = function (tile) {
            // todo: not implemented yet
                    };
        return EditorViewModel;
    })();
    ComicTales.EditorViewModel = EditorViewModel;    
    var TileViewModel = (function () {
        function TileViewModel(id) {
            this.id = id;
            this.imageUrl = '/Content/imgs/tile1.png';
        }
        return TileViewModel;
    })();
    ComicTales.TileViewModel = TileViewModel;    
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=Editor.js.map

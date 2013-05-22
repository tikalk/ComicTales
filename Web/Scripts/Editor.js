/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
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
            alert('add');
        };
        EditorViewModel.prototype.editTile = function (tile) {
            alert('edit ' + tile.id);
        };
        EditorViewModel.prototype.deleteTile = function (tile) {
            alert('delete ' + tile.id);
        };
        return EditorViewModel;
    })();
    ComicTales.EditorViewModel = EditorViewModel;    
    var TileViewModel = (function () {
        function TileViewModel(id) {
            this.id = id;
        }
        return TileViewModel;
    })();
    ComicTales.TileViewModel = TileViewModel;    
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=Editor.js.map

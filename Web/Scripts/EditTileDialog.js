/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
var ComicTales;
(function (ComicTales) {
    var EditTileDialog = (function () {
        function EditTileDialog(tile) {
            this.tile = tile;
            this.title = tile ? 'Edit Tile' : 'Add New Tile';
        }
        EditTileDialog.prototype.open = function () {
            $('#editTileDialog').dialog({
                title: this.title,
                width: 800,
                height: 600,
                modal: true
            });
        };
        return EditTileDialog;
    })();
    ComicTales.EditTileDialog = EditTileDialog;    
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=EditTileDialog.js.map

/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
var ComicTales;
(function (ComicTales) {
    var EditTileDialog = (function () {
        function EditTileDialog(tile) {
            this.tile = tile;
            this.isCameraVisible = ko.observable(true);
            EditTileDialog.ensureDialogCreated();
            this.title = tile ? 'Edit Tile' : 'Add New Tile';
            this.isNew = !tile;
            this.selectedView = ko.observable(this.isNew ? 'takePicture' : 'edtiTile');
        }
        EditTileDialog.dialogCreated = false;
        EditTileDialog.ensureDialogCreated = function ensureDialogCreated() {
            if(!EditTileDialog.dialogCreated) {
                // create dialog once
                $('#editTileDialog').dialog({
                    autoOpen: false,
                    width: 800,
                    height: 600,
                    modal: true,
                    close: // remove binding on close
                    function () {
                        EditTileDialog.stopCamera();
                        ko.cleanNode($('#editTileDialog')[0]);
                    }
                });
            }
        };
        EditTileDialog.prototype.open = function () {
            // apply binding on open
            ko.applyBindings(this, $('#editTileDialog')[0]);
            $('#editTileDialog').dialog('option', 'title', this.isNew ? 'Add New Tile' : 'Edit Tile').dialog('open');
        };
        EditTileDialog.prototype.openCamera = function () {
            // show camera
            this.selectedView('takePictureFromCamera');
            this.isCameraVisible(true);
            var video = $('#takePictureFromCamera video')[0];
            (navigator).webkitGetUserMedia({
                video: true
            }, function (stream) {
                video.src = (window).webkitURL.createObjectURL(stream);
                video.play();
            }, function (error) {
                return console.log("Video capture error: ", error.code);
            });
        };
        EditTileDialog.stopCamera = function stopCamera() {
        };
        EditTileDialog.prototype.takeCameraSnapshot = function () {
            var video = $('#takePictureFromCamera video')[0];
            var canvas = $('#takePictureFromCamera canvas')[0];
            var context = canvas.getContext("2d");
            context.drawImage(video, 0, 0, 640, 480);
            EditTileDialog.stopCamera();
            // show snapshot
            this.isCameraVisible(false);
        };
        return EditTileDialog;
    })();
    ComicTales.EditTileDialog = EditTileDialog;    
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=EditTileDialog.js.map

var ComicTales;
(function (ComicTales) {
    var EditTileDialog = (function () {
        function EditTileDialog(okCallback, tile) {
            this.okCallback = okCallback;
            this.tile = tile;
            this.isCameraVisible = ko.observable(true);
            EditTileDialog.ensureDialogCreated();

            this.title = tile ? 'Edit Tile' : 'Add New Tile';
            this.isNew = !tile;

            this.selectedView = ko.observable(this.isNew ? 'takePicture' : 'edtiTile');
        }
        EditTileDialog.ensureDialogCreated = function () {
            if (!EditTileDialog.dialogCreated) {
                $('#editTileDialog').dialog({
                    autoOpen: false,
                    width: 800,
                    height: 600,
                    modal: true,
                    close: function () {
                        EditTileDialog.stopCamera();
                        ko.cleanNode($('#editTileDialog')[0]);
                    }
                });
            }
        };

        EditTileDialog.prototype.open = function () {
            ko.applyBindings(this, $('#editTileDialog')[0]);

            $('#editTileDialog').dialog('option', 'title', this.isNew ? 'Add New Tile' : 'Edit Tile').dialog('open');
        };

        EditTileDialog.prototype.openCamera = function () {
            this.selectedView('takePictureFromCamera');
            this.isCameraVisible(true);

            var video = $('#takePictureFromCamera video')[0];

            (navigator).webkitGetUserMedia({ video: true }, function (stream) {
                video.src = (window).webkitURL.createObjectURL(stream);
                video.play();
            }, function (error) {
                return console.log("Video capture error: ", error.code);
            });
        };

        EditTileDialog.stopCamera = function () {
        };

        EditTileDialog.prototype.takeCameraSnapshot = function () {
            var video = $('#takePictureFromCamera video')[0];
            var canvas = $('#takePictureFromCamera canvas')[0];

            var context = canvas.getContext("2d");
            context.drawImage(video, 0, 0, 640, 480);

            EditTileDialog.stopCamera();

            this.isCameraVisible(false);
        };
        EditTileDialog.dialogCreated = false;
        return EditTileDialog;
    })();
    ComicTales.EditTileDialog = EditTileDialog;
})(ComicTales || (ComicTales = {}));

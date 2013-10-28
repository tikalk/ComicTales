/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

module ComicTales {
    export class EditTileDialog {

        private title: string;
        private isNew: boolean;

        public selectedView: KnockoutObservable<string>;
        public isCameraVisible = ko.observable(true);
        public imageUrl = ko.observable("../Content/imgs/empty.png");

        constructor(
            private okCallback: (tile: TileViewModel) => void,
            private tile?: TileViewModel) {

            EditTileDialog.ensureDialogCreated();

            this.title = tile ? 'Edit Tile' : 'Add New Tile';
            this.isNew = !tile;

            this.selectedView = ko.observable(this.isNew ? 'takePicture' : 'edtiTile');

        }

        private static dialogCreated = false;
        private static ensureDialogCreated(): void {
            if (!EditTileDialog.dialogCreated) {
                // create dialog once
                $('#editTileDialog').dialog({
                    autoOpen: false,
                    width: 800,
                    height: 600,
                    modal: true,
                    // remove binding on close
                    close: () => {
                        EditTileDialog.stopCamera();
                        ko.cleanNode($('#editTileDialog')[0]);
                    },
                })
            }
        }


        public open(): void {

            // apply binding on open
            ko.applyBindings(this, $('#editTileDialog')[0]);

            $('#editTileDialog')
                .dialog('option', 'title', this.isNew ? 'Add New Tile' : 'Edit Tile')
                .dialog('open');
        }


        public saveSnapshot(snapshot): void {

            //snapshot = snapshot.replace('data:image/png;base64,', '');
            $.post('/Story/123/SaveSnapshot', { dataURL: snapshot },

            (data, status) => {
                //alert("Data: " + data + "\nStatus: " + status);

                this.imageUrl(data);
                this.selectedView('edtiTile');
            });

        }

        public openCamera(): void {

            // show camera
            this.selectedView('takePictureFromCamera');
            this.isCameraVisible(true);

            var video = <HTMLVideoElement> $('#takePictureFromCamera video')[0];

            (<any>navigator).webkitGetUserMedia(
                { video: true },
                (stream) => {
                    video.src = (<any>window).webkitURL.createObjectURL(stream);
                    video.play();
                },
                (error) => console.log("Video capture error: ", error.code)
            );

        }

        public static stopCamera(): void {
        }

        public takeCameraSnapshot(): void {

            var video = <HTMLVideoElement> $('#takePictureFromCamera video')[0];
            var canvas = <HTMLCanvasElement>$('#takePictureFromCamera canvas')[0];

            var context = canvas.getContext("2d");
            context.drawImage(video, 0, 0, 640, 480);

            EditTileDialog.stopCamera();

            // show snapshot
            this.isCameraVisible(false);
        }

        public saveTile(): void {

            var tile = {
                imageUrl: this.imageUrl()
            };

            this.okCallback(tile);

            $('#editTileDialog').dialog("close");
        }
    }
}
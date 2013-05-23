/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

module ComicTales {
    export class EditTileDialog {

        private title: string;

        constructor(private tile?: TileViewModel) {

            this.title = tile ? 'Edit Tile' : 'Add New Tile';
        }

        public open(): void {

            $('#editTileDialog').dialog({
                title: this.title,
                width: 800,
                height: 600,
                modal: true,
            });
        }
    }
}
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />

interface JQueryStatic {
    connection: any;
}

module ComicTales {

    export function Init(storyId: string): EditorViewModel{

        var viewModel = new EditorViewModel(storyId);

        ko.applyBindings(viewModel);

        return viewModel;

    }

    export class EditorViewModel {

        public tiles = ko.observableArray([]);
        public isLoading = ko.observable(false);
        public hasUpdates = ko.observable(false);
        public editTileDialog: EditTileDialog;

        constructor(private storyId: string) {

            // load data
            this.loadTiles();

            // Start the connection
            this.initConnection();
        }

        public refresh(): void {
            this.loadTiles();
            this.hasUpdates(false);
        }

        public addNewTile(): void {

            this.editTileDialog = new EditTileDialog((tile) => {
                this.saveTile(tile);
            });
            
            this.editTileDialog.open();

        }

        public editTile(tile: TileViewModel) {
            this.editTileDialog = new EditTileDialog((t) => { }, tile);
            
            this.editTileDialog.open();
        }

        public deleteTile(tile: TileViewModel) {
            // todo: not implemented yet
        }

        public saveTile(tile: TileViewModel) {
            $.post('/Story/' + this.storyId + '/AddTile', tile, () => {
                this.notifyUpdated();
                this.refresh();
            });
        }

        public saveStory()
        {
            $.post('/Story/' + this.storyId + '/Save',() => this.notifyUpdated());
        }

        private notifyUpdated()
        {
            $.connection.comicStoryNotificationsHub.server.notifyHasUpdates(this.storyId);
        }

        private loadTiles() {
            this.isLoading(true);
            $.get('/Story/' + this.storyId + '/GetTiles', (data, textStatus, jqXHR) => {
                this.isLoading(false);
                this.updateTiles(data);
            });
        }

        private updateTiles(data) {
            this.tiles.removeAll();
            $.each(data.tiles, (index, tile) => this.tiles.push(tile));
        }

        private initConnection() {

            // Proxy created on the fly 
            var storyNotifications = $.connection.comicStoryNotificationsHub;

            // Declare a function on the chat hub so the server can invoke it          
            storyNotifications.client.notifyHasUpdates = () => {
                console.log('Recieve updates notification!');
                this.hasUpdates(true);
            };

            // Start the connection
            $.connection.hub.start()
                .done(() => {
                    console.log('Now connected, connection ID=' + $.connection.hub.id + ', transport=' + $.connection.hub.transport.name);
                    storyNotifications.server.join(this.storyId);
                })
                .fail(() => { console.log('Could not Connect!'); });
        }
    }

    export interface TileViewModel {

        imageUrl: string;
    }
}
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from '@angular/material';

import components from './components';
import services from './services';
import hubs from './hubs';

@NgModule({
    imports:
    [
        BrowserModule,
		MaterialModule
    ],
    declarations:
    [
		...components.declarations
	],
	providers: [
		...services,
		...hubs
	],
    bootstrap: [components.bootstrap]
})
export default  class AppModule { }
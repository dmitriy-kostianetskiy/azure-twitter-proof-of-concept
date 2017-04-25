import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import components from './components';
import services from './services';
import hubs from './hubs';

@NgModule({
    imports:
    [
		BrowserModule,
	    BrowserAnimationsModule,
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
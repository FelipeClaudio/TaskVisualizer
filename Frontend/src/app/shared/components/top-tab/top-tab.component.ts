import { Component, inject } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-top-tab',
    templateUrl: './top-tab.component.html',
    styleUrls: ['./top-tab.component.scss'],
    imports: [MatTabsModule, MatIconModule]
})

export class TopTabComponent {
    constructor() {
        const iconRegistry = inject(MatIconRegistry);
        const sanitizer = inject(DomSanitizer);
        iconRegistry.addSvgIcon('task-icon', sanitizer.bypassSecurityTrustResourceUrl("../../../assets/task-icon.svg"));
        iconRegistry.addSvgIcon('reward-icon', sanitizer.bypassSecurityTrustResourceUrl("../../../assets/reward-icon.svg"));
        iconRegistry.addSvgIcon('calendar-icon', sanitizer.bypassSecurityTrustResourceUrl("../../../assets/calendar-icon.svg"));
    }
}

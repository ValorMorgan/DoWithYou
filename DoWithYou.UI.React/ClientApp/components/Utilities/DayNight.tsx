import * as React from 'react';
import { SunClock } from './Clock';
import { ICommonProps } from './Misc';

interface IDayNightProps extends ICommonProps {
    dayStart?: number;
    dayEnd?: number;
}

export class DayNight extends React.Component<IDayNightProps, {}> {
    constructor(props: IDayNightProps) {
        super(props);
    }

    render() {
        let {dayStart, dayEnd, ...other} = this.props;

        ({ dayStart, dayEnd } = this.ValidateDayStartAndDayEnd(dayStart, dayEnd));

        return (
            <div id="dayNight">
                <SunClock {...other}
                    dayStart={dayStart}
                    dayEnd={dayEnd}
                    UpdateShadowOffset={this.UpdateShadowOffset}/>
            </div>
        );
    }

    private ValidateDayStartAndDayEnd = (dayStart: number | undefined, dayEnd: number | undefined) => {
        // Default values if not provided
        if (!dayStart)
            dayStart = 6;
        if (!dayEnd)
            dayEnd = 18;

        // Confirm dayStart is before DayEnd (switch if not)
        if (dayStart > dayEnd) {
            let temp = dayEnd;
            dayEnd = dayStart;
            dayStart = temp;
            // If same, reduce dayStart by 1 hour
        }
        else if (dayStart == dayEnd && dayStart >= 1) {
            dayStart -= 1;
            // If same and no room to reduce dayStart, set dayEnd to end of day (or 24:00)
        }
        else if (dayStart == dayEnd) {
            dayEnd = 24;
        }

        return { dayStart, dayEnd };
    }
    
    UpdateShadowOffset = (position: number, arc: number) => {
        // H-Offset
        const hOffsetCap = 6;
        const hOffset = `${(position > 50 ? -1 : 1 ) * (hOffsetCap * (position / 100))}px`;
        document.documentElement.style.setProperty('--box-shadow-h-offset', hOffset);

        // V-Offset
        const vOffsetCap = 8;
        const vOffset = `${vOffsetCap * ((100 / arc) / 100)}px`;
        document.documentElement.style.setProperty('--box-shadow-v-offset', vOffset);
    }
}
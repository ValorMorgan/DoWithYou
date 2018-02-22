import * as React from 'react';
import { ICommonProps } from './Misc';
import { Sun } from './Sun';

const timeInterval: number = 1000;

interface IClockState {
    date: Date;
}

export class DigitalClock extends React.Component<ICommonProps, IClockState> {
    timerID: number = -1;
    
    constructor(props: ICommonProps) {
        super(props);

        this.state = { date: new Date() };
    }

    componentDidMount() {
        this.timerID = setInterval(
            () => this.tick(),
            timeInterval
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    tick() {
        this.setState({
            date: new Date()
        });
    }

    render() {
        return (
            <div className="clock clock-digital">
                <p className="clock-digital-time">{this.state.date.toLocaleTimeString()}</p>
            </div>
        );
    }
}

interface IClockSunState extends IClockState {
    isDay: boolean;
    position: number;
}

const dayStart: number = 6;
const dayEnd: number = 18;

export class SunClock extends React.Component<ICommonProps, IClockSunState> {
    timerID: number = -1;
    dayCycle: number = dayEnd - dayStart;
    
    constructor(props: ICommonProps) {
        super(props);

        this.state = {
            date: new Date(),
            isDay: true,
            position: 100
        };
    }

    componentDidMount() {
        this.timerID = setInterval(
            () => this.tick(),
            timeInterval
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    tick() {
        this.setState({
            date: new Date(),
            isDay: true,
            position: 0
        });

        const hours = this.state.date.getHours();
        if (!hours) {
            return;
        }
        
        console.log(hours + '/' + this.dayCycle);
        // Daytime is:
        // 6:00 AM (06:00) - 5:59 PM (17:59)
        this.setState({
            isDay: hours >= dayStart && hours < dayEnd,
            position: 100 - Math.round((hours / dayEnd) * 100)
        });

        const $ = require('jquery');
        $('.sun').css('margin-left', `${this.state.position}%`);
    }

    render() {
        return (
            <div className="clock clock-sun">
                {this.state.isDay ? <Sun /> : <p>Moon</p>}
            </div>
        );
    }
}
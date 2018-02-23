import * as React from 'react';
import { Image } from './Image';
import { ICommonProps } from './Misc';

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
                <p className="clock-digital__time">{this.state.date.toLocaleTimeString()}</p>
            </div>
        );
    }
}

interface IClockSunProps extends ICommonProps {
    dayStart: number;
    dayEnd: number;
}

interface IClockSunState extends IClockState {
    isDay: boolean;
    position: number;
    arc: number;
}

export class SunClock extends React.Component<IClockSunProps, IClockSunState> {
    timerID: number = -1;

    constructor(props: IClockSunProps) {
        super(props);

        this.state = {
            date: new Date(),
            isDay: true,
            position: 100, // %
            arc: 1 // %
        };
    }

    render() {
        let style: React.CSSProperties = {
            marginLeft: this.state.position+'%',
            marginTop: this.state.arc+'%'
        }

        return (
            <div className="clock clock-sun">
                {this.state.isDay ?
                    <Image src="" className="clock-sun__timer sun" style={style} /> :
                    <Image src="" className="clock-sun__timer moon" style={style} />}
            </div>
        );
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

        let time = this.state.date.getHours() + (this.state.date.getMinutes() / 60);
        if (!time) {
            return;
        }
        
        this.setState({
            isDay: this.isDay(time),
            position: this.getPosition(time),
            arc: this.getArc()
        });
    }

    isDay = (time: number): boolean => {
        return time >= this.props.dayStart && time <= this.props.dayEnd;
    }

    getPosition = (time: number): number => {
        let dayTime: number = this.props.dayEnd-this.props.dayStart;
        let nightTime: number = 24-dayTime;

        return this.isDay(time) ?
            /* Day */   100 - (((time - this.props.dayStart) / dayTime) * 100) :
            /* Night */ 100 - ((Math.abs(time - this.props.dayEnd) / nightTime) * 100);
    }

    getArc = (): number => {
        return this.state.position < 25 ?
                17 * (1 - (this.state.position / 25)) :
            this.state.position > 75 ?
                17 * ((this.state.position - 74) / 25) :
                1; // 1%
    }
}
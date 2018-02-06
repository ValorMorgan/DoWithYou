import * as React from 'react';
import { ICommonProps } from './Misc';

interface IClockState {
    date: Date;
}

export class DigitalClock extends React.Component<ICommonProps, IClockState> {
    timerID: number;
    interval: number = 1000;
    
    constructor(props: ICommonProps) {
        super(props);

        this.state = { date: new Date() };
    }

    componentDidMount() {
        this.timerID = setInterval(
            () => this.tick(),
            this.interval
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
            <div className="clock-digital">
                <p className="clock-digital-time">{this.state.date.toLocaleTimeString()}</p>
            </div>
        );
    }
}
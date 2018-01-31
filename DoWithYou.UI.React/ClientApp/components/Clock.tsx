import * as React from 'react';

interface IClockProps {
}

interface IClockState {
    date: Date;
}

abstract class Clock extends React.Component<IClockProps, IClockState> {
    timerID: number;
    interval: number = 1000;
    
    constructor() {
        super();

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
}

export class DigitalClock extends Clock {
    constructor() {
        super();
    }

    render() {
        return (
            <div className="clock-digital">
                <p className="clock-digital-time">{this.state.date.toLocaleTimeString()}</p>
            </div>
        );
    }

    componentDidMount() {
        super.componentDidMount();
    }

    componentWillUnmount() {
        super.componentWillUnmount();
    }
}
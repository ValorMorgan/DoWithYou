import * as React from 'react';
import { NavMenu } from './NavMenu';
import { Header } from './Header';

export interface ILayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<ILayoutProps, {}> {
    render() {
        return (
            <div id='layout' className='grid'>
                <NavMenu />
                <Header/>
                <div id='body'>
                    {this.props.children}
                </div>
            </div>
        );
    }
}
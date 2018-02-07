import * as React from 'react';
import * as Misc from './Utilities/Misc';
import { NavMenu } from './NavMenu';
import { Header } from './Utilities/Header';

export interface ILayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<ILayoutProps, {}> {
    render() {
        return (
            <div id='layout' className='container-fluid'>
                <div className='row'>
                    <div id='layout-left' className='col-md-4'>
                        <NavMenu />
                    </div>
                    <Misc.ClearFix />
                    <div id='layout-right' className='col-md-8'>
                        <Header />
                        <div id='body'>
                            {this.props.children}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
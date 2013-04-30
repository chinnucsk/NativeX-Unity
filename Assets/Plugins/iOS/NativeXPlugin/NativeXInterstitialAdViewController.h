//
//  NativeXInterstitialAdViewController.h
//  NativeXPublisherSdk
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Bozhidar Mihaylov on 12/22/11.
//  Copyright (c) 2011 MentorMate. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol NativeXInterstitialAdViewControllerDelegate;
@interface NativeXInterstitialAdViewController : UIViewController

@property (nonatomic, unsafe_unretained) id<NativeXInterstitialAdViewControllerDelegate> delegate;
@property (nonatomic, copy) NSNumber *themeID;

@property (nonatomic, readonly, getter = isAdLoading) BOOL adLoading;
@property (nonatomic, readonly, getter = isAdLoaded) BOOL adLoaded;

//this can be set as an alternative to implementing a delegate
//(Modal view presents on and dismisses from this property if not nil)
@property (nonatomic, unsafe_unretained) UIViewController *parentController;

//Controls the presentation style of the view controller
// Set to YES if you want modal presentation and NO if you use transparency 

@property (nonatomic, assign) BOOL shouldPresentModally;

//the modal view can be set to auto dismiss after a set amount of time
@property (nonatomic, assign) NSTimeInterval autoDismissTime;

//the modal view can be set to landscape mode (defaults to portrait)
@property (nonatomic, assign) BOOL landscapeOrientation;

//whether view controller to handle status bar when visible (hide on present and show on dismiss).
@property (nonatomic, assign) BOOL handleStatusBar;

//animation for hiding status bar (default to no animation)
@property (nonatomic, assign) UIStatusBarAnimation statusBarHideAnimation;

//the close round button exposed for customization
@property (nonatomic, readonly) UIButton *closeButton;

+ (id)interstitialAdViewControllerWithThemeID:(NSNumber *)themeID 
                                     delegate:(id<NativeXInterstitialAdViewControllerDelegate>)delegate;
- (id)initWithThemeID:(NSNumber *)themeID 
             delegate:(id<NativeXInterstitialAdViewControllerDelegate>)delegate;

- (void)reloadAdContent;

- (void)presentFromViewController:(UIViewController *) presentingViewController;

//popover style for ipad
- (void)presentPopoverFromRect:(CGRect)rect 
                        inView:(UIView *)view 
      permittedArrowDirections:(UIPopoverArrowDirection)arrowDirections  __deprecated;

- (void)presentPopoverFromBarButtonItem:(UIBarButtonItem *)item 
               permittedArrowDirections:(UIPopoverArrowDirection)arrowDirections __deprecated;

@end

@protocol NativeXInterstitialAdViewControllerDelegate <NSObject>

@required
- (void)didLoadContentForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView;
- (void)noAdContentForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView;
- (void)interstitialAdViewController:(NativeXInterstitialAdViewController *)adView
                    didFailWithError:(NSError *)error;

@optional
// called when view allows its delegate to dismiss it when it's done
- (void)dismissActionForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView;
// called when modal or popover view disappear from screen
- (void)didDismissForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView;

@end
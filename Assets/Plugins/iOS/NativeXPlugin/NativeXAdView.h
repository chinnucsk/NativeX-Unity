//
//  NativeXAdView.h
//  NativeXMonetizationSdk
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Ash Lindquist on 6/4/13.
//  Copyright (c) 2013 NativeX. All rights reserved.
//

#import <UIKit/UIKit.h>

/** Ad placement type. */
typedef enum
{
    /// Ad is placed in application content.
    nativeXAdViewPlacementTypeInline = 0,
    
    /// Ad is placed over and in the way of application content.
    /// Generally used to place an ad between transtions in an application
    /// and consumes the entire screen.
    nativeXAdViewPlacementTypeInterstitial
    
} nativeXAdViewPlacementType;

@protocol NativeXAdViewDelegate;

@interface NativeXAdView : UIView <UIWebViewDelegate>

/** delegate file for ad view. */
@property (nonatomic, weak) id<NativeXAdViewDelegate> delegate;

/** default close button */
@property (nonatomic, readonly) UIButton *closeButton;

/** Placement Type = Interstitial or Inline */
@property (nonatomic, readonly) nativeXAdViewPlacementType placementType;

/** give your ad a placement name to recall it */
@property (nonatomic, strong) NSString *name;

/** View Controller that will be used to present view */
@property (nonatomic, strong) UIViewController *presentingViewController;

/**
 * Advanced: call to initialize an instance of an interstitial ad
 *
 * @param name          optional for setting per placement
 * @param aDelegate     the delegate file to use for this interstitial instance
 *
 * @return              NativeXAdView Ad view with placementType = interstitial
 */
- (id)initInterstitialWithName:(NSString *)name
                        delegate:(id<NativeXAdViewDelegate>)aDelegate;

/** call to display the Interstitial once the content has been loaded */
- (void)displayInterstitial;

/** call to display the Interstitial once the content has been loaded */
- (void)displayInterstitial:(UIViewController *) presentingVC;

/** call to dismiss interstitial */
- (void)dismissInterstitial;

@end

@protocol NativeXAdViewDelegate <NSObject>

@optional
/** Called when adView is loaded and ready to be displayed
 * use this method to override when adView is displayed
 * If this delegate does not exist when caching an ad it will be shown immediately
 *
 * @param adView        the NativeX adView that has been loaded
 * @param name          placement name for ad, use for showing cached ad
 */
- (void)didLoadAdView:(NativeXAdView *)adView withName:(NSString *)name;

/** called if no ad is available at this time
 *
 * @param adView        the NativeX adView that has NOT been loaded
 */
- (void)noAdContentForAdView:(NativeXAdView *)adView;

/** Called when error loading an ad (was the SDK initialized correctly?)
 *
 * @param adView        the NativeX adView that has NOT been loaded because of an error
 * @param error         reason why ad failed to load
 */
- (void)nativeXAdView:(NativeXAdView *)adView didFailWithError:(NSError *)error;

/** Called when ad content has expired for specific adView
 *
 * @param adView        the NativeX adView that has expired
 */
- (void)adContentExpiredForAdView:(NativeXAdView *)adView;


/** Called when SDK needs to get presentingVC for displaying the adView
 * presentingViewController defaults to the keyWindow's rootviewcontroller, 
 * but this method can be used to set a specific parent view controller for the AdView
 *
 * @param adView        the NativeX adView that will be the child view
 * @return              UIViewController the view controller that will be used as parent to adView
 */
- (UIViewController *)presentingViewControllerForAdView:(NativeXAdView *) adView;

/** called right before an ad will be displayed
 *
 * @param adView        the NativeX adView that will be displayed
 */
- (void)nativeXAdViewWillDisplay:(NativeXAdView *)adView;

/** called after an ad has been displayed on screen
 *
 * @param adView        the NativeX adView that was displayed
 */
- (void)nativeXAdViewDidDisplay:(NativeXAdView *)adView;

/** called right before an ad will be dismissed
 *
 * @param adView        the NativeX adView that will be dismissed
 */
- (void)nativeXAdViewWillDismiss:(NativeXAdView *)adView;

/** called after an ad has been dismissed
 *
 * @param adView        the NativeX adView that was dismissed
 */
- (void)nativeXAdViewDidDismiss:(NativeXAdView *)adView;

/** Used for adding dismiss actions for an adView */
- (void)dismissActionForAdView:(NativeXAdView *)adView completionBlock:(void (^)(void))completion;

/** Used for capturing rich media events 
 *
 * @param adView        the NativeX adView for which the rich media request was fired.
 * @param event         The Rick Media event that was fired
 */
- (void)nativeXAdView:(NativeXAdView *)adView didProcessRichmediaRequest:(NSURLRequest*)event;

@end


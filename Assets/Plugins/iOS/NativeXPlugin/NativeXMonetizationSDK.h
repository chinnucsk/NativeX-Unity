//
//  NativeXMonetizationSDK.h
//  NativeXMonetizationSdk
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Patrick Brennan on 10/6/11.
//  Copyright 2011 NativeX. All rights reserved.

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "NativeXBannerAdView.h"
#import "NativeXInterstitialAdViewController.h"
#import "NativeXEnhancedAdView.h"
#import "NativeXInAppPurchaseTrackRequest.h"

@protocol NativeXMonetizationDelegate;

/** Main class for NativeXPublisherSDK */
@interface NativeXMonetizationSDK : NSObject

@property (nonatomic, copy) NSString *gameTitle;
@property (nonatomic, assign) BOOL showViolations;
@property (nonatomic, assign) BOOL showMessages;
@property (nonatomic, strong) id<NativeXMonetizationDelegate> delegate;
@property (nonatomic, strong) UIPopoverController *offerWallPopover;
@property (nonatomic, assign) BOOL shouldShowRedeemAlert;
@property (nonatomic, assign) BOOL shouldBeWebOfferWall;
@property (nonatomic, strong) UIAlertView *featuredAlertView;
@property (nonatomic, readonly) BOOL isShowFeaturedOffer;
@property (nonatomic, assign) int numberOfOffersInOfferWall;
@property (nonatomic, assign) BOOL shouldUseInAppDownload;

#pragma mark - Publisher API

/** 
 This method provides access to the NativeXMonetizationSDK shared object.
 
 @return a singleton NativeXMonetizationSDK instance.
 */
+ (id)sharedInstance;

/** 
 This method provides access to the NativeXMonetizationSDK version
 
 @return NativeXMonetizationSDK Version
 */
- (NSString *)getSDKVersion;

/**
 Create a session with NativeX offer network.
 Call this in AppDidFinishLaunchingWithOptions{}
 
 @param appID is the unique Identifier you receive from NativeX
 @param publisherUserId Id used for publisher currency postback if used
 */
- (void)initiateWithAppId:(NSString *)appId
       andPublisherUserId:(NSString *)publisherUserId;

/**
 Reward Discovery Wall presentation 
 
 @param presentingViewController View Controller the SDK will use as parent to Discovery Wall
 */
- (void)openOfferWallFromPresentingViewController:(UIViewController *)presentingViewController;

/**
 Native iPad reward Discovery Wall presentation
 
 @param popoverView UIView used to position the popOver and direction arrows
 */
- (void)openOfferWallFromPopoverView:(UIView *)popoverView;

/**
 Non-reward web Discovery Wall presentation
 
 @param presentingViewController View Controller the SDK will use as the parent of non-reward discovery wall
 */
- (void)openNonRewardWebOfferWallFromPresentingViewController:(UIViewController *)presentingViewController;

/**
 Gets and shows a featured offer alert
 */
- (void)showFeaturedOffer;

/**
 Gets and caches a featured offer alert
 */
- (void)getAndCacheFeaturedOffer;

/**
 Shows cached featured offer alert
 */
- (void)showCachedFeaturedOffer;

/** 
 Call redeem currency
 */
- (void)redeemCurrency;

#pragma mark - Enhanced Interstitial Ad Methods (MRAID)

/**
 Show an MRAID complient interstitial from key window
 */
- (void)showInterstitialAd;

/**
 Show an MRAID complient interstitial with placement name from key window, used for targeting certain ads for cetain in app placements.
 
 @param name   NSString representation of placement name
 */
- (void)showInterstitialAdWithName:(NSString *)name;

/**
 Cache an MRAID complient interstitial
 
 @param name   NSString representation of placement name (optional)
 @param delegate        used to set delegate
 */
- (void)cacheInterstitialAdWithName:(NSString *) name delegate:(id<NativeXEnhancedAdViewDelegate>)aDelegate;

#pragma mark - Regular Ad methods
/**
 Initialize an instance of Banner Ad
 
 @param themeId optional ID for publisher theme
 @param delegate
 @param frame set size of banner ad
 
 @return NativeXBannerAdView 
 */
- (NativeXBannerAdView *)bannerAdViewWithThemeID:(NSNumber *)themeID
                                        delegate:(id<NativeXBannerAdViewDelegate>)delegate
                                           frame:(CGRect)frame;

/**
 Initialize an instance of Interstitial Ad
 
 @param themeId optional ID for publisher theme
 @param delegate
 
 @return NativeXInterstitialAdView
 */
- (NativeXInterstitialAdViewController *)interstitialAdViewControllerWithThemeID:(NSNumber *)themeID
                                                                        delegate:(id<NativeXInterstitialAdViewControllerDelegate>)delegate;

#pragma mark - In App Purchase Tracking (IAPT) methods
/**
 Used for In App Purchase Tracking
 
 @param trackRecord
 @param delegate
 
 @return NativeXInAppPurchaseRequest
 */
- (NativeXInAppPurchaseTrackRequest *)trackInAppPurchaseWithTrackRecord:(NativeXInAppPurchaseTrackRecord *)trackRecord
                                                               delegate:(id<NativeXInAppPurchaseTrackDelegate>)delegate; //if the delegate is about to be deallocated clear return value's delegate property


/**
 Use this method to get sessionId for current session
 @return the current sessionId
 */
- (NSString *) getSessionId;


#pragma mark - NativeX Advertiser API

/**
 call this to connect to NativeX and inform that the app "appID" was run
 call this in AppDidFinishLaunchingWithOptions
 
 @param appID -- is the unique Application Identifier you receive from NativeX
*/
- (void)connectWithAppID:(NSString*)appID;

/**
 call this to connect to NativeX and inform that the app "actionID" was performed
 @param actionID is the unique Action Identifier for the action, that you receive from NativeX
 */
- (void)actionTakenWithActionID:(NSString*)actionID;

/**
 call this when you no longer need the shared connector object, usually in AppWillTerminate
 */
- (void)close;

@end

#pragma mark - Publisher Delegates

@protocol NativeXMonetizationDelegate <NSObject>

@required

/** Called when the Offer Wall is successfully initialized. 
 @param isAvailable -- boolean flag to let developer know if discovery wall is available
 */
- (void)nativeXMonetizationSdkDidInitiateWithIsOfferwallAvailable:(BOOL)isAvailable;

/** Called when there is an error trying to initialize the Offer Wall. 
 @param error
 */
- (void)nativeXMonetizationSdkDidFailToInitiate: (NSError *) error;

/** Called when the currency redemption is successfull. 
 @param balances -- an array
 @param recieptId unique identifier for currency redeption
 */
- (void)didRedeemWithBalances:(NSArray *)balances
                 andReceiptId:(NSString *)receiptId;

/** Called when the currency redemption is unsuccessfull. 
 @param error
 */
- (void)didRedeemWithError:(NSError *)error;

/** Called when publisher is about to display modally fullscreen instruction view for a chosen featured offer 
 @param interstitialInstructionVC   This VCs view will be added at the child to the Vc that is returned in this method.
 @return UIViewController -- View Controller used to present modal interstitial screens
 */
- (UIViewController *)parentViewControllerForModallyPresentingInterstitialInstructionView:(UIViewController *)interstitialInstructionVC;

@optional
/** Called when offerwall is about to display */
- (void)offerWallWillDisplay;

/** Called after offerwall did display*/
- (void)offerWallDidDisplay;

/** Called when offerwall is about to dismiss */
- (void)offerWallWillDismiss;

/** Called after offerwall did dismiss */
- (void)offerWallDidDismiss;

/** Called when offerwall or ad offers are about to redirect
 *
 * @deprecated please use "SDKWillRedirectUser"
 */
- (void)offerWallWillRedirectUserToAppStore __deprecated;

/** Called when redirecting user away from application (safari or app store) */
- (void)SDKWillRedirectUser;

/** Called when a freatured offer alert is not available */
- (void)featuredOfferNotAvailable;

/** Called when a featured offer alert is available */
- (void)featuredOfferIsAvailable;

/** Called after featured offer alert did dismiss */
- (void)featuredOfferDidDismiss;

//TODO: add enhanceAd delegate AdWillShow method so devs can pause game play
- (void)adWillShow;

@end
